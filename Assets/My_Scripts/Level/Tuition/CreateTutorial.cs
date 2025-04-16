using UnityEngine;

public class CreateTutorial : MonoBehaviour
{
    [SerializeField] private string headerHeirarchy; // Variable assigned to the heirarchy path for the header text component
    [SerializeField] private string headerText; // Variable assigned to a specific tutorial header value in the spection window
    [SerializeField] private string shortDescriptionHeirarchy; // Variable assigned to the heirarchy path for the short description text component
    [SerializeField] private string shortDescriptionText; // Variable assigned to a specific tutorial short description value in the spection window
    [SerializeField] private TuitionData tuitionData; // Variable assigned to the TuitionData script component.

    // Will be called when the player enters a tutorial trigger box
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the collider and call the SpawnTutorial method
        if (other.CompareTag("Player"))
        {
            tuitionData.SpawnTutorial(); // Instantiate tutorial
            tuitionData.SetTextValue(headerHeirarchy, headerText); // Set header value
            tuitionData.SetTextValue(shortDescriptionHeirarchy, shortDescriptionText); // Set short description value
        }
    }
}
