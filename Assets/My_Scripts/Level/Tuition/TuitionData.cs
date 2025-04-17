using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TuitionData : MonoBehaviour
{
    public bool tutorialActive = false; // Variable used to check if there is a tutorial already rendered
    public GameObject smallTutorialPrefab; // Variable assigned to the small tutorial game object prefab
    public GameObject largeTutorialPrefab; // Variable assigned to the large tutorial game object prefab
    public Transform canvasTransform; // Variable assigned to the UI canvas

    [SerializeField] private GameObject smallTutorial; // Variable assigned to the newly instantiated small tutorial game object
    [SerializeField] private GameObject largeTutorial; // Variable assigned to the newly instantiated large tutorial game object

    // Method to spawn the tutorial UI when the player enters a tutorial trigger collider
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

    // Method used to toggle the active property on both the small and large tutorials - effectively swapping them.
    public void ToggleActiveTutorial()
    {   
        // Set both tutorials active properties to the inverse of themselves
        smallTutorial.SetActive(!smallTutorial.activeInHierarchy);
        largeTutorial.SetActive(!largeTutorial.activeInHierarchy);
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

    public void SetVideoClip(string videoPath)
    {
        VideoClip videoClip = Resources.Load<VideoClip>(videoPath);

        Transform videoObject = largeTutorial.transform.Find("Media_Wrapper/Video_Player");
        VideoPlayer videoPlayer = videoObject.GetComponent<VideoPlayer>();

        videoPlayer.clip = videoClip;
    }
}
