using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AttackType
{
    public string trigger;
    public float cooldown;
    public float instantiateTimeout;
    public GameObject bullet;
    public Vector3 position;
    public float destroyTime;
}

public class UnitController : MonoBehaviour
{
    public float speed = 120;
    public float moveAfterAttackPause = 0.4f;
    public float maxHealth = 100;
    public bool isPlayer = false;
    public List<AttackType> attacks = new List<AttackType>();

    public Vector3 Velocity = new Vector3();

    Animator animatorMove;
    Animator animatorAttack; // null for mobs

    bool isFacingRight = true;

    bool isDead = false;
    float health;
    float attackTime = 0f;
    float moveTime = 0f;

    void Start()
    {
        animatorMove = transform.Find("MoveSprite").GetComponent<Animator>();
        if (isPlayer)
        {
            animatorAttack = transform.Find("AttackSprite").GetComponent<Animator>();
        }

        health = maxHealth;
    }

    void Update()
    {
        if (moveTime > 0)
            moveTime -= Time.deltaTime;

        if (attackTime > 0)
            attackTime -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (isDead)
            return;

        if (moveTime <= 0)
            rigidbody.velocity = Velocity * speed * Time.deltaTime;

        float dirSpeed = rigidbody.velocity.x;
        animatorMove.SetFloat("Speed", Mathf.Max(Mathf.Abs(dirSpeed), Mathf.Abs(rigidbody.velocity.z)));
        if (dirSpeed > 0 && !isFacingRight ||
            dirSpeed < 0 && isFacingRight)
            Flip();
    }

    public void SetAttackPause(float pause)
    {
        attackTime = pause;
    }

    public bool CanAttack()
    {
        return !isDead && attackTime <= 0;
    }

    public void Attack(string trigger)
    {
        var attack = attacks.Find(a => a.trigger == trigger);
        if (attack != null &&  CanAttack())
        {
            Debug.Log((isPlayer ? "Player" : "Bot") + " attacks with: " + attack.trigger);
            rigidbody.velocity = new Vector3();
            moveTime = moveAfterAttackPause;
            attackTime = attack.cooldown;

            var animator = isPlayer ? animatorAttack : animatorMove;
            animator.SetTrigger(attack.trigger);
            StartCoroutine("InstantiateAttack", attack);
        }
    }

    IEnumerator InstantiateAttack(AttackType attack)
    {
        yield return new WaitForSeconds(attack.instantiateTimeout);

        Vector3 pos = transform.position;
        pos.x += attack.position.x * transform.localScale.x;
        pos.y += attack.position.y * transform.localScale.y;
        pos.z += attack.position.z * transform.localScale.z;
        var obj = Instantiate(attack.bullet, pos, Quaternion.identity) as GameObject;

        obj.transform.parent = transform;
        obj.tag = isPlayer ? "PlayerAttack" : "BotAttack";
        Destroy(obj, attack.destroyTime);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void RecieveDamage(float damage)
    {
        health -= damage;
        Debug.Log("Recieved damage: " + damage + ";   health: " + health);
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            animatorMove.SetBool("IsDead", true);
            if (animatorAttack != null)
                animatorAttack.SetBool("IsDead", true);
        }
    }

    public bool SwitchWeapon()
    {
        if (animatorAttack.GetCurrentAnimatorStateInfo(0).IsName("Katana Idle") ||
            animatorAttack.GetCurrentAnimatorStateInfo(0).IsName("Gun Idle") ||
            animatorAttack.GetCurrentAnimatorStateInfo(0).IsName("Grenade Idle"))
        {
            animatorAttack.SetTrigger("SwitchWeapon");
            return true;
        }
        return false;
    }
}
