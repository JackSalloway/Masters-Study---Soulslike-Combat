using TMPro;
using UnityEngine;

public class TuitionData : MonoBehaviour
{
    public GameObject smallTutorialPrefab; // Variable assigned to the small tutorial game object prefab
    public Transform canvasTransform; // Variable assigned to the UI canvas

    [SerializeField] private GameObject activeTutorial; // variable assigned to the newly instantiated tutorial game object.

    public void SpawnTutorial()
    {
        if (activeTutorial != null) return; // Early return if the value of activeTutorial is already a game object.

        activeTutorial = Instantiate(smallTutorialPrefab, canvasTransform); // Instantiate the small tutorial
    }

    // Method to delete the tutorial UI when the player presses the E key or leaves the relevant tutorials bounds 
    public void DeleteTutorial()
    {
        Destroy(activeTutorial); // Destroy the tutorial game object
        activeTutorial = null; // Reset the value of activeTutorial to null
    }

    // Method used to set an individual text value of a UI component, takes two strings as its parameters.
    // heirarchyPath is a string that refers to the path in the heirarchy window from the currently rendered tutorial to the relevant child that needs to be updated.
    // desiredText is a string that contains the value of the text being set. 
    public void SetTextValue(string heirarchyPath, string desiredText)
    {
        Transform textObject = activeTutorial.transform.Find(heirarchyPath);
        TextMeshProUGUI headerText = textObject.GetComponent<TextMeshProUGUI>();
        headerText.text = desiredText;
    }
}
