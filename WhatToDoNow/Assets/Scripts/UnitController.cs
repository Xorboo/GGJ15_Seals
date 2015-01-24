using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour
{
    public float speed = 120;
    public float moveAfterAttackPause = 0.4f;
    public float attackCooldown = 0.7f;
    public float maxHealth = 100;

    public Vector3 Velocity = new Vector3();

    float health;
    float attackTime = 0f;
    float moveTime = 0f;

    void Start()
    {
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
        if (moveTime <= 0)
            rigidbody.velocity = Velocity * speed * Time.deltaTime;
    }

    public bool CanAttack()
    {
        return attackTime <= 0;
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
}
