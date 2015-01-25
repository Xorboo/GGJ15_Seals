using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveUpdateTime;
    public float buttonPressTime;

    UnitController controller;
    GameController gameController;
	// Use this for initialization
	void Start() {
        controller = GetComponent<UnitController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        StartCoroutine("MoveCoroutine");
	}

    void Update()
    {
    }


    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            UpdateControls();
            yield return new WaitForSeconds(moveUpdateTime);
        }
    }

    void UpdateControls()
    {
        controller.Velocity = gameController.GetMoveDirection();
    }

    // Temp function for 1 user control
    void UpdateControlsKeyboard()
    {
        controller.Velocity.Set(0, 0, 0);
        if (Input.GetKey(KeyCode.S))
            controller.Velocity.z -= 1;
        if (Input.GetKey(KeyCode.W))
            controller.Velocity.z += 1;
        if (Input.GetKey(KeyCode.A))
            controller.Velocity.x -= 1;
        if (Input.GetKey(KeyCode.D))
            controller.Velocity.x += 1;
        controller.Velocity.Normalize();

        if (controller.CanAttack())
        {
            if (Input.GetMouseButton(0))
                controller.Attack();
        }
    }
}
