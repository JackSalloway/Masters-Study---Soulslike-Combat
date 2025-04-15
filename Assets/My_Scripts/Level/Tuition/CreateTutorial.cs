using UnityEngine;

public class CreateTutorial : MonoBehaviour
{
    [SerializeField] private TuitionData tuitionData; // Variable assigned to the TuitionData script component. 

    // Will be called when the player enters a tutorial trigger box
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the collider and call the SpawnTutorial method
        if (other.CompareTag("Player")) tuitionData.SpawnTutorial();
    }
}
