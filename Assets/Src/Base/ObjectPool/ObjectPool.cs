using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    static Dictionary<Type, Queue<GameObject>> _poolMap;
    static float countCreation;
    static float countCall;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<Type, Queue<GameObject>> _poolMap = new Dictionary<Type, Queue<GameObject>>();
        countCreation = 0;
        countCall = 0;
    }

    public static GameObject Instantiate(Type objType, Vector3 position, Quaternion rotation)
    {
        Debug.Log("[ObjectPool] ask for creation " + objType + " " + position + " " + rotation);
        Queue<GameObject> _pool = _poolMap[objType];

        countCall++;
        countCreation += (_pool.Count == 0 ? 1 : 0);
        Debug.Log("[ObjectPool] creation called" + countCall + "\t" + countCreation + "\t" + (countCall - countCreation));

        if (_pool.Count == 0)
        {
            GameObject instance = (GameObject)Activator.CreateInstance(objType);
            return UnityEngine.Object.Instantiate(instance, position, rotation);
        }
        else
        {
            GameObject instance = _pool.Dequeue();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }
    }

    public static void Retrieve(GameObject gameObj)
    {
        gameObj.SetActive(false);
        Type objType = gameObj.GetType();
        if (_poolMap.ContainsKey(objType)) _poolMap.Add(objType, new Queue<GameObject>());
        Queue<GameObject> _pool = _poolMap[objType];
        _pool.Enqueue(gameObj);
    }
}
