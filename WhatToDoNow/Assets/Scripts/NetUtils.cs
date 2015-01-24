using UnityEngine;
using System.Collections;

public static class NetUtils
{
    public static string ColorMessage(Color color)
    {
        return "COLOR " + color.r + " " + color.g + " " + color.b;
    }
}
