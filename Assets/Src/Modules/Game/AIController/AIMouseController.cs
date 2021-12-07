using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character;
using Weapons;
using BayatGames.Serialization.Formatters.Json;
using Photon.Pun;

public class AIMouseController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] float _nerfScale;

    [SerializeField] CharacterStats _characterStats;
    [SerializeField] Camera _eye;

    [SerializeField] AmmoWeaponStats _defaultWeaponStats;
    [SerializeField] GameObject _gun;
    IWeapon _gunEngine;
    GameObject _target;

    Vector3 walkPoint;
    bool _isPatrolPointSet;

    [SerializeField] float _attackRange;
    [SerializeField] GameObject _model;

    Vector3 _lastPos;

    PhotonView view;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        view = this.GetComponent<PhotonView>();

        if (this._gun.GetComponent<MeleeWeapon>() != null) this._gunEngine = this._gun.GetComponent<MeleeWeapon>();
        else if (this._gun.GetComponent<AmmoWeapon>() != null) this._gunEngine = this._gun.GetComponent<AmmoWeapon>();
    }

    void Start()
    {
        agent.speed = _characterStats.Speed * _nerfScale;
        this.RecordPos();
    }

    void LateUpdate()
    {
        if (view.IsMine)
        {
            bool playerInSight = this.IsPlayerInSight;
            bool playerIsNear = this.IsPlayerIsNear;

            if (!playerInSight) Patrol();
            else
            {
                this._isPatrolPointSet = false;
                if (!playerIsNear) Chase();
                else Attack();
            }

            _lastPos = this.transform.position;
        }
    }

    void Patrol()
    {
        if (this._gunEngine.NeedReload()) this._gunEngine.TriggerReload();
        if (!_isPatrolPointSet) SearchWalkPoint();

        if (_isPatrolPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude <= 4) _isPatrolPointSet = false;
    }

    void SearchWalkPoint()
    {
        if (Spawner.Ins == null) return;
        List<Vector3> cucumberPos = Spawner.Ins.CucumberPoints.ConvertAll(cucumber => cucumber.transform.position);
        cucumberPos = cucumberPos.FindAll(pos => Vector3.Distance(this.transform.position, pos) > 1);

        if (cucumberPos.Count > 0)
        {
            Vector3 randomPos = Utils.PickFromList<Vector3>(cucumberPos);
            walkPoint = randomPos;
            _isPatrolPointSet = true;
            Debug.Log("Heading to cucumber ", cucumberPos.IndexOf(randomPos) + 1);
        }
    }

    void Chase()
    {
        Debug.Log("Chase", this._target);
        if (this._gunEngine.NeedReload()) this._gunEngine.TriggerReload();
        this.GetComponent<Eye>().LookAt(this._target);
        agent.SetDestination(this._target.transform.position);
    }

    void Attack()
    {
        if (MathUtils.RandomInt(0, 4) == 0) this.Chase();
        else agent.SetDestination(this.transform.position);

        Debug.Log("Attack", this._target);
        this.GetComponent<Eye>().LookAt(this._target);
        if (MathUtils.RandomInt(0, 9) == 0) this._gunEngine.TriggerAttack();
    }

    void RecordPos()
    {
        this._lastPos = this.transform.position;
        LeanTween.delayedCall(5, () =>
        {
            this.RecordPos();
        });
    }

    bool IsPlayerInSight
    {
        get
        {
            List<GameObject> mice = CharacterMgr.Ins.Characters.FindAll(go =>
                go.GetComponent<CharacterStats>().CharacterSide == CharacterSide.CATS
            );

            this._target = null;
            while (mice.Count > 0)
            {
                GameObject mouse = Utils.PickFromList(mice, true);
                MeshRenderer renderer = mouse.GetComponent<Eye>().CharModel.GetComponent<MeshRenderer>();
                if (this._eye.IsObjectVisible(renderer))
                {
                    this._target = mouse;
                    return true;
                }
            }
            return false;
        }
    }

    bool IsPlayerIsNear
    {
        get
        {
            if (this._target == null) return false;
            else
            {
                float distance = Vector3.Distance(this._target.transform.position, this.transform.position);
                Debug.Log("Distance to target", distance, this._gunEngine);
                return (distance <= this._attackRange);
            }
        }
    }
}
