using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUtils
{
    public static long NowButModded()
    {
        return DateTimeOffset.Now.ToUniversalTime().ToUnixTimeSeconds() % long.MaxValue;
    }

    public static long Now()
    {
        return DateTimeOffset.Now.ToUniversalTime().ToUnixTimeSeconds();
    }
}
