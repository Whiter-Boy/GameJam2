
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public CharacterController controller;
    public GameObject player2;
    public Behaviour VHS;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = player2.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) 
            Patroling();
<<<<<<< HEAD
        if (playerInSightRange && !playerInAttackRange && controller.velocity.magnitude == 0)
=======
        if (playerInSightRange && !playerInAttackRange)
>>>>>>> a72a8e4fd9a7beda0a2cda54266d1e5a7bb0c6f3
            ChasePlayer();
            //VHS.gameObject.GetComponent<VHSPostProcessEffect>().EnableEffect = false;

        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        VHS.enabled = false;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
<<<<<<< HEAD
        if (controller.velocity.magnitude == 0)
        {
            agent.SetDestination(player.position);
            VHS.enabled = true;
        }
        else
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            VHS.enabled = false;
        }
=======
        agent.SetDestination(player.position);
        VHS.enabled = true;
>>>>>>> a72a8e4fd9a7beda0a2cda54266d1e5a7bb0c6f3
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}