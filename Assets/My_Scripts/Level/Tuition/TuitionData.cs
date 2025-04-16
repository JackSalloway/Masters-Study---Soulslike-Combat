using TMPro;
using UnityEngine;

public class TuitionData : MonoBehaviour
{
    public float holdDuration = 2f;
    public float holdTimer = 0f;

    public GameObject smallTutorialPrefab; // Variable assigned to the small tutorial game object prefab
    public GameObject largeTutorialPrefab; // Variable assigned to the large tutorial game object prefab
    public Transform canvasTransform; // Variable assigned to the UI canvas

    [SerializeField] private bool tutorialActive = false; // Variable used to check if there is a tutorial already rendered
    [SerializeField] private GameObject smallTutorial; // Variable assigned to the newly instantiated small tutorial game object
    [SerializeField] private GameObject largeTutorial; // Variable assigned to the newly instantiated large tutorial game object

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (tutorialActive == false) return; // Early return if no tutorials are currently active

            holdTimer += Time.deltaTime;

            if (holdTimer >= holdDuration)
            {
                smallTutorial.SetActive(!smallTutorial.activeInHierarchy);
                largeTutorial.SetActive(!largeTutorial.activeInHierarchy);
                holdTimer = 0;
            }
        }
        else
        {
            holdTimer = 0;
        }
    }

    public void SpawnTutorials()
    {
        if (tutorialActive == true) return; // Early return if there are already tutorials active

        smallTutorial = Instantiate(smallTutorialPrefab, canvasTransform); // Instantiate the small tutorial
        largeTutorial = Instantiate(largeTutorialPrefab, canvasTransform); // Instantiate the large tutorial
        tutorialActive = true; // Set the value of tutorialActive to true
    }

    // Method to delete the tutorial UI when the player presses the E key or leaves the relevant tutorials bounds 
    public void DeleteTutorials()
    {
        Destroy(smallTutorial); // Destroy the small tutorial game object
        Destroy(largeTutorial); // Destroy the large tutorial game object
        smallTutorial = null; // Reset the value of smallTutorial to null
        largeTutorial = null; // Reset the value of largeTutorial to null
        tutorialActive = false; // Reset the value of tutorialActive to false
    }

    // Method used to set an individual text value of a UI component in the small tutorial, takes two strings as its parameters.
    // hierarchyPath is a string that refers to the path in the hierarchy window from the currently rendered tutorial to the relevant child that needs to be updated.
    // desiredText is a string that contains the value of the text being set. 
    public void SetSmallTutorialTextValue(string hierarchyPath, string desiredText)
    {
        Transform textObject = smallTutorial.transform.Find(hierarchyPath);
        TextMeshProUGUI headerText = textObject.GetComponent<TextMeshProUGUI>();
        headerText.text = desiredText;
    }

    // Method used to set an individual text value of a UI component in the large tutorial, takes two strings as its parameters.
    // hierarchyPath is a string that refers to the path in the hierarchy window from the currently rendered tutorial to the relevant child that needs to be updated.
    // desiredText is a string that contains the value of the text being set. 
    public void SetLargeTutorialTextValue(string hierarchyPath, string desiredText)
    {
        Transform textObject = largeTutorial.transform.Find(hierarchyPath);
        TextMeshProUGUI headerText = textObject.GetComponent<TextMeshProUGUI>();
        headerText.text = desiredText;
    }
}
