using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool playerInArena = false; // Variable to store whether the player is currently in the arena or not

    [Header("Script References")]
    [SerializeField] private CombatTuitionData combatTutionData; // Reference the CombatTuitionData script

    // Set playerInArena to true when the player enters the collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArena = true;
            combatTutionData.playerEnteredArena = true; // Spawn the first combat tutorial
        }
    }

    // Set playerInArena to false when the player exits the collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInArena = false;
    }
}
