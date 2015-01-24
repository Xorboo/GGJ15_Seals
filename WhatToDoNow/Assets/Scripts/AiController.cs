using UnityEngine;
using System.Collections;

public class AiController : MonoBehaviour
{
    public float attackDistance;

    UnitController controller;
    Transform player;

    void Start()
    {
        controller = GetComponent<UnitController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine("DirectionUpdate");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<UnitController>().RecieveDamage(1);
        }
    }

    IEnumerator DirectionUpdate()
    {
        while (true)
        {
            controller.Velocity.Set(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);
            yield return new WaitForSeconds(0.3f);
        }
    }
}