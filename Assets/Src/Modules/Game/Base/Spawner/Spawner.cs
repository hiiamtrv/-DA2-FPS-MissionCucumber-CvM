using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _prefabCat;
    [SerializeField] GameObject _prefabMouse;

    [SerializeField] List<GameObject> _mouseSpawns;
    [SerializeField] List<GameObject> _catSpawns;

    public void DoSpawn(CharacterSide startSide)
    {
        switch (startSide)
        {
            case CharacterSide.MICE:
                Debug.Log("Spawn character mouse");
                GameVar.Ins.Player = SpawnCharacter(_prefabMouse, _mouseSpawns);
                break;
            case CharacterSide.CATS:
                Debug.Log("Spawn character cat");
                GameVar.Ins.Player = SpawnCharacter(_prefabCat, _catSpawns);
                break;
        }
        this.enabled = false;
    }

    GameObject SpawnCharacter(GameObject character, List<GameObject> spawnPoints)
    {
        GameObject spawnPoint = Utils.PickFromList(spawnPoints);

        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Debug.Log("Spawn character", character.name, spawnPosition, randomRotation);
        return PhotonNetwork.Instantiate(character.name, spawnPosition, randomRotation);
    }
}
