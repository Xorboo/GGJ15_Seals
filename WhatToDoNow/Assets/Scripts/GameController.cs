using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    /*public enum PressedButton
    {
        A ==1, B ==2
    };*/
    Serv server;
    
    public void Start()
    {
        server = GetComponent<Serv>();
    }


    public Vector3 GetMoveDirection()
    {
        Vector3 dir = server.MainMoving(); // new Vector3(-10, 0, 15);
        return dir;//.normalized;
    }

    public bool GetButtonPressed(int btn, float maxTimeout)
    {
        return server.ButtonNumChance(btn, maxTimeout) < Random.value;
    }

}
