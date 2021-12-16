using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _prefabCat;
    [SerializeField] GameObject _prefabMouse;

    [SerializeField] GameObject _prefabAICat;
    [SerializeField] GameObject _prefabAIMouse;

    [SerializeField] List<GameObject> _mouseSpawns;
    [SerializeField] List<GameObject> _catSpawns;

    [SerializeField] GameObject _prefabCucumber;

    [SerializeField] List<GameObject> _cucumberPoints;
    public List<GameObject> CucumberPoints => _cucumberPoints;

    [SerializeField] GameObject _mouseRetreatPoint;
    public GameObject MouseRetreatPoint => _mouseRetreatPoint;

    [SerializeField] GameObject _catRetreatPoint;
    public GameObject CatRetreatPoint => _catRetreatPoint;

    static Spawner _ins;
    public static Spawner Ins => _ins;

    HashSet<int> _occupiedCatSpawn;
    HashSet<int> _occupiedMouseSpawn;

    void Awake()
    {
        _ins = this;
        _occupiedCatSpawn = new HashSet<int>();
        _occupiedMouseSpawn = new HashSet<int>();
    }

    public void DoSpawn(CharacterSide startSide, int spawnIndex)
    {
        switch (startSide)
        {
            case CharacterSide.MICE:
                Debug.Log("Spawn character mouse");
                GameVar.Ins.Player = SpawnCharacter(_prefabMouse, _mouseSpawns, spawnIndex);
                this._occupiedMouseSpawn.Add(_mouseSpawns[spawnIndex].GetHashCode());
                break;
            case CharacterSide.CATS:
                Debug.Log("Spawn character cat");
                GameVar.Ins.Player = SpawnCharacter(_prefabCat, _catSpawns, spawnIndex);
                this._occupiedCatSpawn.Add(_catSpawns[spawnIndex].GetHashCode());
                break;
        }
        this.enabled = false;
    }

    GameObject SpawnCharacter(GameObject character, List<GameObject> spawnPoints, int? spawnIndex = null)
    {
        GameObject spawnPoint;
        if (spawnIndex == null) spawnPoint = Utils.PickFromList(spawnPoints, true);
        else spawnPoint = spawnPoints[(int)spawnIndex];

        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion spawnRotation = spawnPoint.transform.rotation;

        Debug.Log("Spawn character", character.name, spawnPosition, spawnRotation);
        return PhotonNetwork.Instantiate(character.name, spawnPosition, spawnRotation);
    }

    public void SpawnCucumbers(List<int> cucumberIndexes)
    {
        if (cucumberIndexes == null) return;
        cucumberIndexes.ForEach(index =>
        {
            Vector3 position = this._cucumberPoints[index].transform.position;
            GameObject cucumber = PhotonNetwork.Instantiate(_prefabCucumber.name, position, Quaternion.identity);
        });
    }

    public void SpawnBots()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            List<GameObject> mouseSpawns = _mouseSpawns.FindAll(item => !_occupiedMouseSpawn.Contains(item.GetHashCode()));
            List<GameObject> catSpawns = _catSpawns.FindAll(item => !_occupiedCatSpawn.Contains(item.GetHashCode()));

            Debug.Log("Spawns remains", mouseSpawns, catSpawns);

            int numPMouse = 0;
            int numPCat = 0;

            foreach (var item in GameVar.Players)
            {
                switch ((CharacterSide)item.Value)
                {
                    case CharacterSide.CATS: numPCat++; break;
                    case CharacterSide.MICE: numPMouse++; break;
                }
            }

            int numBotMouse = NetworkGame.NUM_MICE_SLOT - numPMouse;
            int numBotCat = NetworkGame.NUM_CATS_SLOT - numPCat;

            Debug.Log("Cats team", numPCat, numBotCat);
            Debug.Log("Mice team", numPMouse, numBotMouse);

            for (var i = 0; i < numBotMouse; i++) this.SpawnCharacter(_prefabAIMouse, mouseSpawns);
            for (var i = 0; i < numBotCat; i++) this.SpawnCharacter(_prefabAICat, catSpawns);
        }
    }
}
