using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class Debug
{
    public static void Log(params object[] values)
    {
        string textPrint = "";
        for (int i = 0; i < values.Length; i++)
        {
            textPrint += "{@index} \t".Replace("@index", i.ToString());
        }
        UnityEngine.Debug.LogFormat(textPrint, values);
    }
}