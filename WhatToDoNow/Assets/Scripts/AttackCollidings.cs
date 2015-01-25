using UnityEngine;
using System.Collections;

public class AttackCollidings : MonoBehaviour
{
    public float damage;
    bool haveCollided = false;

    void OnTriggerEnter(Collider other)
    {
        if (haveCollided)
            return;

        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Debug.Log("COLLIDER ATTACK: this: " + tag + " ; other: " + other.tag);
            if ((tag == "PlayerAttack") != (other.tag == "Player"))
            {
                other.GetComponent<UnitController>().RecieveDamage(damage);
                haveCollided = true;
                Destroy(gameObject);
            }
        }
    }
}
