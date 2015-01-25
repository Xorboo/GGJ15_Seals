using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*namespace Assets.Scripts
{*/
    /*enum Moves
    { 
        None,
        Left,
        Right,
        Up,
        Down
    };
    Moves moves = Moves.None;
     */
public class UserKeys// : MonoBehaviour
{
    public int moveX=0, moveY=0;
    public int numButton=0;
    //public DateTime dt = DateTime.Now;
    public float timeA = 0.0f;
    public float timeB = 0.0f;
    public UserKeys()
    {
        moveX = 0; moveY = 0;
        numButton = 0;
        //dt = DateTime.Now;
    }
    // "1,1,1" или "-1,-1,2" или "0,0,0"
    // первое число 1,3-2,4 
    private void setDatasFromString(String str)
    {
        List<String> lst = str.Split(',').ToList();
        lst.Remove("");
        switch (Convert.ToInt32(lst[0]))
        {
            case 1: moveX = 0; moveY = 1; break;
            case 2: moveX = 1; moveY = 0; break;
            case 3: moveX = 0; moveY = -1; break;
            case 4: moveX = -1; moveY = 0; break;
            default: moveX = 0; moveY = 0; break;
        };
        int b2 = Convert.ToInt32(lst[2]);
        int b1 = Convert.ToInt32(lst[1]);
        if (b1 != 0)
        {
            timeA = Time.time;
            numButton = b1;
        }
        if (b2 != 0)
        {
            timeB = Time.time;
            numButton = b2;
        }
        if ((b2 == b1) && (b1 == 0))
            numButton = 0;
    }
    public UserKeys(String str)
    {
        setDatasFromString(str);
    }
    public void ReRead(String str)
    {
        setDatasFromString(str);
    }
}
//}
