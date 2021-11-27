using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVar : MonoBehaviour
{
    [SerializeField] GameObject _player;
    public GameObject Player => _player;

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
}
