using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour
{
    public Vector3 startVelocity;
    public float grav;
    public float blowHeight;

    SphereCollider collider;

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
    }

    void Start()
    {
        rigidbody.velocity = startVelocity;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y - grav * Time.deltaTime, rigidbody.velocity.z);

        if (rigidbody.velocity.y < 0 && transform.position.y < blowHeight)
        {
            Blow();
        }
    }

    void Blow()
    {
        collider.enabled = true;
        Destroy(gameObject, 0.1f);
    }
}