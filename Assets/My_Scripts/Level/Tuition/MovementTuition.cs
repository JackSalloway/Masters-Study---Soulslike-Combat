using UnityEngine;

public class MovementTuition : MonoBehaviour
{
    public GameObject smallTutorialPrefab; // Variable assigned to the small tutorial game object prefab
    public Transform canvasTransform; // Variable assigned to the UI canvas

    [SerializeField] private TuitionData tuitionData; // Variable assigned to the tuition data. 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the collider
        if (other.CompareTag("Player"))
        {
            if (tuitionData.tutorialActive == true) return; // Early return if the tutorial UI is already rendered

            GameObject newImage = Instantiate(smallTutorialPrefab, canvasTransform); // Instantiate the small tutorial
            tuitionData.tutorialActive = true; // Set the tutorialActive variable to true to prevent more than one image rendering at once
        }
    }
}
