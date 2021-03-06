﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveUpdateTime;
    public float buttonPressTime;
    public float weaponSwitchPause;

    UnitController controller;
    GameController gameController;

    int currentAttack = 0;

	void Start() {
        controller = GetComponent<UnitController>();
        controller.isPlayer = true;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        StartCoroutine("MoveCoroutine");
	}

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (gameController.initServer)
                UpdateControls();
            else
                UpdateControlsKeyboard();

            if (controller.CanAttack())
            {
                bool pressShoot = gameController.GetButtonPressed(2, buttonPressTime);
                bool pressSwitch = gameController.GetButtonPressed(1, buttonPressTime);

                if (pressShoot && pressSwitch)
                {
                    if (Random.value <= 0.5)
                        pressShoot = false;
                    else
                        pressSwitch = false;
                }

                if (pressShoot)
                    MakeAttack();

                if (pressSwitch)
                {
                    if (controller.SwitchWeapon())
                    {
                        controller.SetAttackPause(weaponSwitchPause);
                        currentAttack = (currentAttack + 1) % controller.attacks.Count;
                    }
                }

            }
            yield return new WaitForSeconds(moveUpdateTime);
        }
    }

    void MakeAttack()
    {
        controller.Attack(controller.attacks[currentAttack].trigger);
        string text = currentAttack == 0 ? "Sword_" : "Gun_";
        text += Random.Range(1, 3);
        GameObject.Find(text).GetComponent<AudioSource>().Play();
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
            if (Input.GetKey(KeyCode.Z))
                MakeAttack();

            if (Input.GetKey(KeyCode.X))
            {
                if (controller.SwitchWeapon())
                {
                    controller.SetAttackPause(weaponSwitchPause);
                    currentAttack = (currentAttack + 1) % controller.attacks.Count;
                }
            }
        }
    }
}
