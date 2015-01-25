using UnityEngine;
using System.Collections;

public class AiController : MonoBehaviour
{
    public float attackDistance;

    UnitController controller;
    Transform player;

    public enum MobType
    {
        Melee, Ranged, Boss
    };
    public MobType type;
    public float maxX;

    void Start()
    {
        controller = GetComponent<UnitController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine("DirectionUpdate");
    }

    void Update()
    {
        switch (type)
        {
            case MobType.Melee:
                if (Vector3.Distance(transform.position, player.position) < attackDistance)
                {
                    if (controller.CanAttack())
                        controller.Attack(controller.attacks[0].trigger);
                }
                break;
            case MobType.Ranged:
                float dx = Mathf.Abs(GetDX()) - maxX;
                float dy = Mathf.Abs(GetDZ());
                if (dx <= 1 && dy <= 1)
                {
                    if (controller.CanAttack())
                        controller.Attack(controller.attacks[0].trigger);
                }
                break;
            case MobType.Boss:
                break;
        }
    }

    float GetDX()
    {
        return player.position.x - transform.position.x;
    }
    float GetDZ()
    {
        return player.position.z - transform.position.z;
    }

    IEnumerator DirectionUpdate()
    {
        while (true)
        {
            switch (type)
            {
                case MobType.Melee:
                    controller.Velocity.Set(GetDX(), 0, GetDZ());
                    controller.Velocity.Normalize();
                    break;
                case MobType.Ranged:
                    float dx = Mathf.Abs(GetDX()) - maxX;
                    controller.Velocity.Set(dx > 0 ? GetDX() : 0, 0, GetDZ());
                    break;
                case MobType.Boss:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}