using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

    //gameobject is different from general object
    public static T PickFromList<T>(List<T> list, bool removeAfterPick = false)
    {
        if (list.Count == 0) return default(T);
        else
        {
            int index = MathUtils.RandomInt(0, list.Count - 1);
            T result = list[index];
            if (removeAfterPick) list.RemoveAt(index);
            return result;
        }
    }

    public static void DestroyGO(GameObject gameObject)
    {
        if (gameObject != null
            && gameObject.GetComponent<PhotonView>() != null
            && gameObject.GetComponent<PhotonView>().IsMine
        )
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
