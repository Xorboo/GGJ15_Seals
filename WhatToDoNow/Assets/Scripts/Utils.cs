using UnityEngine;
using System.Collections;

public static class Utils
{
    public static Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
