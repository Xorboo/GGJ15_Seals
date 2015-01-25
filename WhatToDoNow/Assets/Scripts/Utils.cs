using UnityEngine;
using System.Collections;
using System;

public static class Utils
{
    public static Color RandomColor()
    {
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
    public static int CurrentTimeMs()
    {
        DateTime curr = DateTime.Now;
        return curr.Millisecond + 1000 * (curr.Second + 60 * (curr.Minute + 60 * curr.Hour));
    }
}
