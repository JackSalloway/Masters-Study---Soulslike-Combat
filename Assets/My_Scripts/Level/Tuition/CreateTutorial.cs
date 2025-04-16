using UnityEngine;

public class CreateTutorial : MonoBehaviour
{
    [SerializeField] private TuitionData tuitionData; // Variable assigned to the TuitionData script component. 

    // Variables used to store the hierarchy path of each UI game object relevant to the TutorialTriggers parent object
    [Header("Hierarchy Path Strings")]
    [SerializeField] private string headerHierarchy = "Header_Wrapper/Header_Text";
    [SerializeField] private string shortDescriptionHierarchy = "Short_Description_Wrapper/Short_Description_Text";

    // Variables used to store specific text values for each tutorial
    [Header("Text Value Strings")]
    [SerializeField] private string headerText;
    [SerializeField] private string shortDescriptionText;
    
    // Will be called when the player enters a tutorial trigger box
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the collider and call the SpawnTutorial method
        if (other.CompareTag("Player"))
        {
            tuitionData.SpawnTutorial(); // Instantiate tutorial
            tuitionData.SetTextValue(headerHierarchy, headerText); // Set header value
            tuitionData.SetTextValue(shortDescriptionHierarchy, shortDescriptionText); // Set short description value
        }
    }
}
