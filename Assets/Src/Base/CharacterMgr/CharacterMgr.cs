using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMgr : MonoBehaviour
{
    static CharacterMgr _instance;
    public static CharacterMgr Ins => _instance;

    [SerializeField] List<GameObject> _characters;

    void Awake()
    {
        _instance = this;
    }

    public static void StartMatch()
    {

    }

    public static void EndMatch()
    {

    }

    public static void RemoveCharacter(GameObject character)
    {

    }

    public static void AddCharacter(GameObject character)
    {

    }
}
