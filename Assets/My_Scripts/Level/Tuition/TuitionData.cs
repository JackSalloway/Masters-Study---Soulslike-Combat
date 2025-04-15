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

}
