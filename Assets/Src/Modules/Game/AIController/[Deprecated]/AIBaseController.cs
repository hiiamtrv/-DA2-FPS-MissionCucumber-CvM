using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character;
using Weapons;
using BayatGames.Serialization.Formatters.Json;
using Photon.Pun;

public class AIBaseController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] protected CharacterSide _side;
    [SerializeField] protected float _nerfScale;

    [SerializeField] protected CharacterStats _characterStats;
    [SerializeField] protected Camera _eye;

    [SerializeField] protected AmmoWeaponStats _defaultWeaponStats;
    [SerializeField] protected GameObject _gun;
    protected IWeapon _gunEngine;
    protected GameObject _target;

    protected Vector3 walkPoint;
    protected bool _isPatrolPointSet;

    [SerializeField] protected float _noticeRange;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected GameObject _model;

    protected Vector3 _lastPos;
    protected float _moveY = 0;

    protected PhotonView view;

    protected virtual void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        view = this.GetComponent<PhotonView>();

        if (this._gun.GetComponent<MeleeWeapon>() != null) this._gunEngine = this._gun.GetComponent<MeleeWeapon>();
        else if (this._gun.GetComponent<AmmoWeapon>() != null) this._gunEngine = this._gun.GetComponent<AmmoWeapon>();
    }

    protected virtual void Start()
    {
        agent.speed = _characterStats.Speed * _nerfScale;
        this.RecordPos();
    }

    protected virtual void LateUpdate()
    {
        Debug.Log("Path status", agent.pathStatus);
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
        }
    }

    protected virtual void Patrol()
    {
        Debug.Log("Patrol", walkPoint);
        if (this._gunEngine.NeedReload()) this._gunEngine.TriggerReload();

        if (!_isPatrolPointSet) SearchWalkPoint();
        if (_isPatrolPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude <= 4) _isPatrolPointSet = false;
    }

    protected virtual void SearchWalkPoint()
    {
        if (Spawner.Ins == null) return;
        // List<GameObject> cucumbers = Spawner.Ins.CucumberPoints.FindAll(cucumber => cucumber.name.Contains("9") || cucumber.name.Contains("10"));
        List<GameObject> cucumbers = Spawner.Ins.CucumberPoints;
        List<Vector3> cucumberPos = cucumbers.ConvertAll(cucumber => cucumber.transform.position);
        cucumberPos = cucumberPos.FindAll(pos => Vector3.Distance(this.transform.position, pos) > 1);

        if (cucumberPos.Count > 0)
        {
            Vector3 randomPos = Utils.PickFromList<Vector3>(cucumberPos);
            walkPoint = randomPos;
            _isPatrolPointSet = true;
            Debug.Log("Heading to cucumber ", cucumberPos.IndexOf(randomPos) + 1);

        }
    }

    protected virtual void Chase()
    {
        Debug.Log("Chase", this._target);
        if (this._gunEngine.NeedReload()) this._gunEngine.TriggerReload();
        this.GetComponent<Eye>().LookAt(this._target);
        agent.SetDestination(this._target.transform.position);
    }

    protected virtual void Attack()
    {
        if (MathUtils.RandomInt(0, 4) == 0) this.Chase();
        else agent.SetDestination(this.transform.position);

        Debug.Log("Attack", this._target);
        this.GetComponent<Eye>().LookAt(this._target);
        if (MathUtils.RandomInt(0, 9) == 0) this._gunEngine.TriggerAttack();
    }

    protected virtual void RecordPos()
    {
        Debug.Log("Record pos", this.transform.position, this._lastPos);
        if (this._lastPos == this.transform.position) this._isPatrolPointSet = false;
        this._lastPos = this.transform.position;
        LeanTween.delayedCall(Time.fixedDeltaTime, () =>
        {
            this.RecordPos();
        });
    }

    protected virtual bool IsPlayerInSight
    {
        get
        {
            List<GameObject> enemies = CharacterMgr.Ins.Characters.FindAll(go =>
                go.GetComponent<CharacterStats>().CharacterSide != this._side
            );

            this._target = null;
            while (enemies.Count > 0)
            {
                GameObject enemy = Utils.PickFromList(enemies, true);
                MeshRenderer renderer = enemy.GetComponent<Eye>().CharModel().GetComponent<MeshRenderer>();
                if (enemy.activeInHierarchy && (this._eye.IsObjectVisible(renderer) || this.IsPlayerIsNoticeable(enemy)))
                {
                    this._target = enemy;
                    return true;
                }
            }
            return false;
        }
    }

    protected virtual bool IsPlayerIsNear
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

    protected virtual bool IsPlayerIsNoticeable(GameObject gameObject)
    {
        Vector3 direction = this.transform.position - gameObject.transform.position;
        float distance = direction.magnitude;

        return Vector3.Distance(this.transform.position, gameObject.transform.position) <= this._noticeRange
        && !Physics.Raycast(this.gameObject.transform.position, direction, distance, LayerMask.GetMask("Map"));
    }
}
