using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameVar : MonoBehaviour
{
    // [SerializeField] CharacterSide _startSide;
    // public CharacterSide StartSide => _startSide;

    static Dictionary<string, int> _players;
    public static Dictionary<string, int> Players { get => _players; set => _players = value; }

    static CharacterSide _startSide;
    public static CharacterSide StartSide { get => _startSide; set => _startSide = value; }

    static List<int> _cucumberIndex;
    public static List<int> CucumberIndex { get => _cucumberIndex; set => _cucumberIndex = value; }

    [SerializeField] GameObject _player;
    public GameObject Player
    {
        get => _player;
        set
        {
            if (_player != null)
            {
                this.GetComponent<CharacterMgr>().RemoveCharacter(_player);
            }
            _player = value;
        }
    }

    [SerializeField] bool _friendlyFire;
    public bool FriendlyFire => _friendlyFire;

    [SerializeField] float _matchDuration;
    public float MatchDuration => _matchDuration;

    [SerializeField] bool _selfDamage;
    public bool SelfDamage => _selfDamage;

    static GameVar _ins;
    public static GameVar Ins => _ins;

    void Awake()
    {
        _ins = this;
    }

    void Start()
    {
        Debug.Log("Do spawn character", _startSide);
        this.GetComponent<Spawner>().DoSpawn(_startSide);
        LeanTween.delayedCall(Time.fixedDeltaTime, () =>
        {
            EventCenter.Publish(EventId.CREATE_PLAYER);
        });

        this.GetComponent<Spawner>().SpawnBots();

        this.GetComponent<Spawner>().SpawnCucumbers(_cucumberIndex);
    }
}
