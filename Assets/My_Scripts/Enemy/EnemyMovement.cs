using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemyAgent; // Reference to the navmesh agent component on the enemy game object
    [SerializeField] private Vector3 patrolPointA; // One side of the arena
    [SerializeField] private Vector3 patrolPointB; // Other side of the arena
    [SerializeField] private Vector3 currentTarget; // Current destination for the agent to head towards
    [SerializeField] private float patrolTolerance = 1f; // Distance to begin turning to other patrol point (tolerance to avoid issues with precision)
    [SerializeField] private Transform player; // Refernce to the players transform, used to get the location of the player character
    [SerializeField] private float detectionRange = 3f; // Variable to store the value that changes the animation to attack when close enough

    [Header("Script References")]
    [SerializeField] private DetectPlayer detectPlayer; // Reference to the DetectPlayer script
    [SerializeField] private EnemyAnimationController enemyAnimController; // Reference to the EnemyAnimationController scipt
    [SerializeField] private EnemyStats enemyStats; // Reference to the EnemyStats script

    // Set patrol variable values when the script starts
    void Awake()
    {
        patrolPointA = new Vector3(22, transform.position.y, -11);
        patrolPointB = new Vector3(22, transform.position.y, 3);
        currentTarget = patrolPointA;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy is dead/stunned/staggered and early return to prevent any movement
        if (enemyStats.isDead ||
            enemyAnimController.GetAnimatorStateValue(EnemyAnimationState.Stunned) ||
            enemyAnimController.GetAnimatorStateValue(EnemyAnimationState.Staggered)) 
        {
            DisableMovement();
            return; // Early return to prevent any more code being ran
        }

        // Ensure the enemy is able to move if the running animation is playing
        if (enemyAnimController.GetAnimatorStateValue(EnemyAnimationState.Running)) EnableMovement();

        // If the player is in the arena, move towards them. Otherwise, patrol between two points
        if (detectPlayer.playerInArena) 
        {   
            float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Get distance to player

            // If distanceToPlayer is less than or equal to detectionRange varaible - trigger attack
            if (distanceToPlayer <= detectionRange)
            {
                // Check if the enemy is facing the player
                Vector3 angleToPlayer = player.position - transform.position; // Get the rotation angle from enemy to player
                angleToPlayer.y = 0; // Flatten the y value as it isn't important here
                float acceptableAngle = Vector3.Angle(transform.forward, angleToPlayer); // Calculate remaining angle from enemy local forward to player position

                // If roughly facing the player, trigger an attack
                if (acceptableAngle < 30f)
                {
                    DisableMovement();
                    enemyAgent.velocity = Vector3.zero; // Remove any residual movement to prevent sliding
                    enemyAnimController.SetAnimatorState(EnemyAnimationState.Attack); // Set attack animation
                }
                else // Otherwise, rotate enemy to face player
                {
                    Quaternion lookRotation = Quaternion.LookRotation(angleToPlayer); // Get look rotation
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f ); // Slowly rotate the Enemy
                }
                
            }
            else if (enemyAnimController.GetAnimatorStateValue(EnemyAnimationState.Running)) // Check if the current state is set to Running
            {
                enemyAgent.SetDestination(player.position); // Move towards player
            }
        }
        else
        {
            Patrol();
        }
    }

    // Method to re-enable movement after an animation that prevents it (like attack / stagger / stun)
    public void EnableMovement() => enemyAgent.isStopped = false;

    // Method to disable movement at the start of an animation that should prevnt it (attack / stagger / stun)
    public void DisableMovement() => enemyAgent.isStopped = true;

    // Method to send the agent patrolling between two points
    private void Patrol()
    {
        enemyAgent.SetDestination(currentTarget); // Move towards current target
        enemyAnimController.SetAnimatorState(EnemyAnimationState.Running); // Set running animation

        float distanceToTarget = Vector3.Distance(transform.position, currentTarget); // Get distance to patrol point

        // If remaining distance is less than the tolerance, toggle target points (turn around)
        if (distanceToTarget <= patrolTolerance)
        {
            TogglePatrolTarget();
        }
    }

    // Method to toggle the current patrol target point
    private void TogglePatrolTarget()
    {
        if (currentTarget == patrolPointA) currentTarget = patrolPointB;
        else currentTarget = patrolPointA;
    }
}
