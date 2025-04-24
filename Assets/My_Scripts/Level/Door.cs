using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool playerInRange = false; // Is the player within range of the door?
    
    [SerializeField] private bool isOpen = false; // Is the door open?
    [SerializeField] private float openAngle; // How far the the door should open (rotate)
    [SerializeField] private float openSpeed; // How fast the door should open

    // Method to set the value of isOpen to true and trigger the coroutine to open the door
    public void Interact()
    {
        if (isOpen) return; // Early return if the door is already open.
        isOpen = true;
        StartCoroutine(OpenDoor());
    }

    // Coroutine to open the door
    private System.Collections.IEnumerator OpenDoor()
    {
        Quaternion startRotation = transform.localRotation; // Declare startRotation value of the door
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0); // Declare targetRotation value of the door (only updates Y axis)
        float slerpProgress = 0f; // Variable used to track progress of Slerp and break the while loop
        
        // Rotate door while slerpProgress is less than 1f
        while (slerpProgress < 1f)
        {
            slerpProgress += openSpeed * Time.deltaTime; // Increment slerpProgress slowly each loop
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, slerpProgress); // Linearly rotate door
            yield return null;
        }
    }

    // Change the state of playerInRange to true when the player enters the collider near the door
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player")) playerInRange = true;
    }

    // Change the state of playerInRange to false when the player leaves the collider near the door
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
