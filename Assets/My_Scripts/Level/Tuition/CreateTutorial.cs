using System.Threading.Tasks;
using UnityEngine;

public class CreateTutorial : MonoBehaviour
{
    [SerializeField] private TuitionData tuitionData; // Variable assigned to the TuitionData script component. 

    // Variables used to store the hierarchy path of each UI game object relevant to the TutorialTriggers parent object
    [Header("Hierarchy Path Strings")]
    [SerializeField] private string headerHierarchy = "Header_Wrapper/Header_Text";
    [SerializeField] private string shortDescriptionHierarchy = "Short_Description_Wrapper/Short_Description_Text";
    [SerializeField] private string extraDescriptionHierarchy = "Extra_Description_Wrapper/Extra_Description_Text";
    [SerializeField] private string optionsHierarchy = "Options_Wrapper/Options_Text";

    // Variables used to store specific text values for each tutorial
    [Header("Text Value Strings")]
    [SerializeField] private string headerText;
    [SerializeField] private string shortDescriptionText;
    [SerializeField] private string extraDescriptionText;
    [SerializeField] private string smallOptionsText;
    [SerializeField] private string largeOptionsText;
    [SerializeField] private string videoClipPath;
    
    // Will be called when the player enters a tutorial trigger box
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the collider and call the SpawnTutorial method
        if (other.CompareTag("Player"))
        {
            tuitionData.SpawnTutorials(); // Instantiate both the small and large tutorials

            // Set the values for the small tutorial
            tuitionData.SetSmallTutorialTextValue(headerHierarchy, headerText); // Set header value
            tuitionData.SetSmallTutorialTextValue(shortDescriptionHierarchy, shortDescriptionText); // Set short description value
            tuitionData.SetSmallTutorialTextValue(optionsHierarchy, smallOptionsText); // Set options value

            // Set the values for the large tutorial
            tuitionData.SetLargeTutorialTextValue(headerHierarchy, headerText); // Set header value
            tuitionData.SetLargeTutorialTextValue(shortDescriptionHierarchy, shortDescriptionText); // Set short description value
            tuitionData.SetLargeTutorialTextValue(extraDescriptionHierarchy, extraDescriptionText); // Set extra description value
            tuitionData.SetLargeTutorialTextValue(optionsHierarchy, largeOptionsText); // Set options value
            tuitionData.SetVideoClip(videoClipPath);
        }
    }
}
