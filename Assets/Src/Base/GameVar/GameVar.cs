using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVar : MonoBehaviour
{
    [SerializeField] CharacterSide _startSide;
    public CharacterSide StartSide => _startSide;

    GameObject _player;
    public GameObject Player
    {
        get => _player;
        set
        {
            if (_player != null)
            {
                this.GetComponent<CharacterMgr>().RemoveCharacter(_player);
                Destroy(_player);
            }
            _player = value;
            this.GetComponent<CharacterMgr>().AddCharacter(_player);
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
        Debug.Log("Do spawn character");
        this.GetComponent<Spawner>().DoSpawn(this._startSide);
    }
}
