using UnityEngine;

public class DeleteTutorial : MonoBehaviour
{
    [SerializeField] private TuitionData tuitionData; // Variable assigned to the TuitionData script component. 

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the collider and call the DeleteTutorial method
        if (other.CompareTag("Player")) tuitionData.DeleteTutorial();
    }
}
