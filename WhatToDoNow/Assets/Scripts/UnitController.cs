using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour
{
    public float speed = 120;
    public float moveAfterAttackPause = 0.4f;
    public float attackCooldown = 0.7f;
    public float maxHealth = 100;

    public Vector3 Velocity = new Vector3();

    Animator animator;
    bool isFacingRight = true;

    bool isDead = false;
    float health;
    float attackTime = 0f;
    float moveTime = 0f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

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
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(dirSpeed), Mathf.Abs(rigidbody.velocity.z)));
        if (dirSpeed > 0 && !isFacingRight ||
            dirSpeed < 0 && isFacingRight)
            Flip();
    }

    public bool CanAttack()
    {
        return !isDead && attackTime <= 0;
    }

    public void Attack()
    {
        if (CanAttack())
        {
            rigidbody.velocity = new Vector3();
            moveTime = moveAfterAttackPause;
            attackTime = attackCooldown;

            // TODO Attack
        }
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
        if (health <= 0)
        {
            health = 0;
            isDead = true;
            animator.SetBool("IsDead", true);
        }
    }
}
