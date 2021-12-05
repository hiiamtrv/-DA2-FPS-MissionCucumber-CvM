using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character;
using Weapons;
using BayatGames.Serialization.Formatters.Json;

public class AICatController : MonoBehaviour
{
    public NavMeshAgent agent;

    [SerializeField] float nerfScale;

    [SerializeField] CharacterStats characterStats;
    [SerializeField] AmmoWeaponStats defaultWeaponStats;

    List<GameObject> _playerInSight;
    Transform target;

    [SerializeField] LayerMask layerGround;

    Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;

    float timeBetweenAttacks;
    bool alreadyAttacked;

    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    [SerializeField] GameObject _model;

    Vector3 _lastPos;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        walkPointRange = characterStats.Speed * 2;
        timeBetweenAttacks = 1 / defaultWeaponStats._fireRate * nerfScale;

        agent.speed = characterStats.Speed * nerfScale;
    }

    void Update()
    {
        bool playerInSight = this.IsPlayerInSight;
        bool playerInAtkRange = this.IsPlayerInRange;

        if (!playerInSight && !playerInAtkRange) Patrol();
        if (playerInSight && !playerInAtkRange) Chase();
        if (playerInSight && playerInAtkRange) Attack();

        _lastPos = this.transform.position;
    }

    void Patrol()
    {
        Debug.Log("Patrol", walkPoint, this.transform.eulerAngles);
        if (
            !walkPointSet ||
            (_lastPos != null && _lastPos == this.transform.position)
        ) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            // LeanTween.rotateAroundLocal(this._model, Vector3.up, 360, Time.deltaTime);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude <= 2) walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        Debug.Log("Search for destination");
        if (Spawner.Ins == null) return;
        List<Vector3> cucumberPos = Spawner.Ins.CucumberPoints.ConvertAll(cucumber => cucumber.transform.position);
        Vector3 randomPos = Utils.PickFromList<Vector3>(cucumberPos);
        if (Vector3.Distance(this.transform.position, randomPos) > 2)
        {
            Debug.Log("Select cucumber point", randomPos);
            walkPoint = randomPos;
            walkPointSet = true;
        }

    }

    void Chase()
    {
        agent.SetDestination(target.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target);
        if (!alreadyAttacked)
        {
            Debug.Log("Attack !");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = true;
    }

    bool IsPlayerInSight
    {
        get
        {
            return false;
        }
    }

    bool IsPlayerInRange
    {
        get
        {
            return false;
        }
    }
}
