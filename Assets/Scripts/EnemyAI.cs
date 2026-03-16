using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Patrol")]
    public Transform[] waypoints;
    private int currentWaypointIndex;

    [Header("Detection")]
    public float detectionRange = 10f;
    public float chaseRange = 12f;

    [Header("Attack")]
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private enum State { Patrol, Chase, Attack }
    private State currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[0].position);
            currentWaypointIndex = 0;
        }

        currentState = State.Patrol;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                if (distanceToPlayer <= detectionRange && CanSeePlayer())
                {
                    currentState = State.Chase;
                }
                else
                {
                    Patrol();
                }
                break;

            case State.Chase:
                if (distanceToPlayer > chaseRange)
                {
                    currentState = State.Patrol;
                }
                else if (distanceToPlayer <= attackRange)
                {
                    currentState = State.Attack;
                }
                else
                {
                    Chase();
                }
                break;

            case State.Attack:
                if (distanceToPlayer > attackRange)
                {
                    currentState = State.Chase;
                }
                else
                {
                    Attack();
                }
                break;
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        transform.LookAt(player);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            lastAttackTime = Time.time;
        }
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
