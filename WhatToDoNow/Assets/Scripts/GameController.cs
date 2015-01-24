using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public enum PressedButton
    {
        A, B
    };

    public Vector3 GetMoveDirection()
    {
        Vector3 dir = new Vector3(-10, 0, 15); // TODO
        return dir.normalized;
    }

    public bool GetButtonPressed(PressedButton btn, float maxTimeout)
    {
        return GetButtonChance(btn, maxTimeout) < Random.value;
    }

    float GetButtonChance(PressedButton btn, float maxTimeout)
    {
        float val = 0f;
        /*foreach userkey
        {
            if (Time.time-userkey.buttontime <= maxTimeout)
                val++;
        }*/
        return val; // return Mathf.Pow(val / userkeys.Count, 4);
    }
}
