using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * transform.localScale.x, transform.position.y, transform.position.z);
    }
}
