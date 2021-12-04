using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static object PickFromList(List<object> listObject, bool removeAfterPick = false)
    {
        if (listObject.Count == 0) return null;
        else
        {
            int index = MathUtils.RandomInt(0, listObject.Count - 1);
            object result = listObject[index];
            if (removeAfterPick) listObject.RemoveAt(index);
            return result;
        }
    }

    //gameobject is different from general object
    public static GameObject PickFromList(List<GameObject> listObject, bool removeAfterPick = false)
    {
        if (listObject.Count == 0) return null;
        else
        {
            int index = MathUtils.RandomInt(0, listObject.Count - 1);
            GameObject result = listObject[index];
            if (removeAfterPick) listObject.RemoveAt(index);
            return result;
        }
    }
}
