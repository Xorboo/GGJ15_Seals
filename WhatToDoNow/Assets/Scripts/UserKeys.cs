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
    public double moveX=0, moveY=0;
    public int numButton=0;
    public DateTime dt = DateTime.Now;
    public UserKeys()
    {
        moveX = 0; moveY = 0;
        numButton = 0;
        dt = DateTime.Now;
    }
    // "1,1,1" или "-1,-1,2" или "0,0,0"
    public UserKeys(String str)
    {
        List<String> lst = str.Split(',').ToList();
        lst.Remove("");
        moveX = Convert.ToDouble(lst[0]);
        moveY = Convert.ToDouble(lst[1]);
        numButton = Convert.ToInt32(lst[2]);
        dt = DateTime.Now;
    }
    public void ReRead(String str)
    {
        List<String> lst = str.Split(',').ToList();
        lst.Remove("");
        moveX = Convert.ToDouble(lst[0]);
        moveY = Convert.ToDouble(lst[1]);
        numButton = Convert.ToInt32(lst[2] == "B" ? 2 : lst[2] == "A"? 1:0);
        dt = DateTime.Now;
    }
}
//}
