using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
    public static int RandomInt(int min, int max)
    {
        float left = min - 0.5f;
        float right = max + 0.5f;
        float varFloat = Random.Range(left, right);
        return Mathf.RoundToInt(Mathf.Clamp(varFloat, min, max));
    }
}