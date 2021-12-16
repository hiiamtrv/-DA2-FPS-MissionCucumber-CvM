using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinecamController : MonoBehaviour
{
    [SerializeField] GameObject _planeAnchor;
    [SerializeField] GameObject[] _catHead;
    [SerializeField] GameObject[] _mouseHead;

    [SerializeField] GameObject[] _catChar;
    [SerializeField] GameObject[] _mouseChar;

    const int LAYER_SELF = 6;

    GameObject _follower;

    void Awake()
    {
        int spawnIndex = GameVar.MySpawnIndex;
        CharacterSide side = GameVar.StartSide;

        switch (side)
        {
            case CharacterSide.CATS:
                _follower = _catHead[spawnIndex];
                Utils.ChangeLayerRecursively(_catChar[spawnIndex], LAYER_SELF);
                break;
            case CharacterSide.MICE:
                _follower = _mouseHead[spawnIndex];
                Utils.ChangeLayerRecursively(_mouseChar[spawnIndex], LAYER_SELF);
                break;
        }
    }

    void Update()
    {
        Debug.Log("Player info", GameVar.StartSide, GameVar.MySpawnIndex);

        GameObject follower = (_follower.activeInHierarchy ? _follower : _planeAnchor);
        transform.position = follower.transform.position;
        transform.rotation = follower.transform.rotation;
    }
}
