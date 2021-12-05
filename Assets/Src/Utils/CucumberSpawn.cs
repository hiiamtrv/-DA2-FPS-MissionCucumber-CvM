using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberSpawn
{
    public static List<int> GetSpawnIndexes()
    {
        int numPos = NetworkGame.NUM_CUCUMBER_POINT;
        int numCucumber = NetworkGame.NUM_CUCUMBER_ON_FIELD;

        List<int> result = new List<int>();
        for (var i = 0; i < numPos; i++) result.Add(i + 1);
        for (var i = 0; i < numPos - numCucumber; i++)
        {
            int discard = Utils.PickFromList<int>(result, true);
            //discard only
        }
        return result;
    }
}
