using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemyAgent; // Reference to the navmesh agent component on the enemy game object
    [SerializeField] private Vector3 originalPosition; // Store the original position of the enemy
    [SerializeField] private Transform player; // Refernce to the players transform, used to get the location of the player character

    [Header("Script References")]
    [SerializeField] private DetectPlayer detectPlayer; // Reference to the DetectPlayer script

    void Awake()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer.playerInArena) enemyAgent.SetDestination(player.position);
        else enemyAgent.SetDestination(originalPosition);
    }
}
