using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float moveUpdateTime;
    Vector3 Velocity = new Vector3();
	// Use this for initialization
	void Start() {
        StartCoroutine("MoveCoroutine");
	}

    void Update()
    {
        //Debug.Log(rigidbody.velocity);
    }

	// Update is called once per frame
	void FixedUpdate() {
        //transform.position.Set(pos.x, pos.y, pos.z);
        rigidbody.velocity = Velocity;
	}

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            UpdateVelocityKeyboard();
            yield return new WaitForSeconds(moveUpdateTime);
        }
    }

    void UpdateVelocity()
    {
        // TODO
    }

    // Temp function for 1 user control
    void UpdateVelocityKeyboard()
    {
        Velocity.Set(0, 0, 0);
        if (Input.GetKey(KeyCode.S))
            Velocity.z -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            Velocity.z += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            Velocity.x -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            Velocity.x += speed * Time.deltaTime;
    }
}
