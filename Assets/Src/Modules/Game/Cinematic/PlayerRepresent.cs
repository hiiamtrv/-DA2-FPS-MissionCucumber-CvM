using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepresent : MonoBehaviour
{
    [SerializeField] CharacterSide _side;
    [SerializeField] int _spawnIndex;
    int _sideInt;

    void Awake()
    {
        this._sideInt = (int)_side;

        foreach (var uid in GameVar.Players.Keys)
        {
            int side = GameVar.Players[uid];
            int spawnIndex = GameVar.SpawnIndexes[uid];

            if (side == _sideInt && spawnIndex == _spawnIndex) return;
        }

        Destroy(this.gameObject);
    }
}
