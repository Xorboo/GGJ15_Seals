using UnityEngine;
using System.Collections;

public class AiController : MonoBehaviour
{
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