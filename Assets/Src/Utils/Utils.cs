using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static void DestroyGO(GameObject gameObject, bool forceDestroy = false)
    {
        if (forceDestroy)
        {
            Object.DestroyImmediate(gameObject);
            return;
        }

        if (gameObject != null)
        {
            if (gameObject.GetComponent<PhotonView>() != null)
            {
                if (gameObject.GetComponent<PhotonView>().IsMine) PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Object.Destroy(gameObject);
            }
        }
    }

    public static string GetSceneNameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public static void ChangeLayerRecursively(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            Utils.ChangeLayerRecursively(child.gameObject, layer);
        }
    }
}
