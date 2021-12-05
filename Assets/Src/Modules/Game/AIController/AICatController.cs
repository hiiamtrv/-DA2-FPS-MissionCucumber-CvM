using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform target;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange;
    public float attackRange;
    public bool playerInSight;
    public bool playerInAtkRange;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAtkRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !playerInAtkRange) Patrol();
        if (playerInSight && !playerInAtkRange) Chase();
        if (playerInSight && playerInAtkRange) Attack();
    }

    void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude <= 1) walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float z = Random.Range(-1, 1) * walkPointRange;
        float x = Random.Range(-1, 1) * walkPointRange;
        walkPoint = transform.position + z * Vector3.forward + x * Vector3.right;
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
}
