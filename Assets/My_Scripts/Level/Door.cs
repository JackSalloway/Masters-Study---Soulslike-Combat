using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool playerInRange = false; // Is the player within range of the door?
    
    [SerializeField] private bool isLocked = true; // Is the door locked? Doors are always locked by default
    [SerializeField] private int keyID = -1; // What is the id of the item required to open the door? Default is an id value that is never used
    [SerializeField] private bool isOpen = false; // Is the door open?
    [SerializeField] private float openAngle; // How far the the door should open (rotate)
    [SerializeField] private float openSpeed; // How fast the door should open
    [SerializeField] private string interactionPrompt = "Interact"; // Variable to describe the action when interacting


    [Header("Script References")]
    [SerializeField] private PlayerInventory playerInventory; // Reference to the PlayerInventory script

    // Unlock the door when the script starts if the ID is -1 
    void Awake()
    {
        if (keyID == -1) isLocked = false;
    }

    // Method that returns the value of the interactionPrompt variable
    public string InteractionPrompt => interactionPrompt;

    // Method to set the value of isOpen to true and trigger the coroutine to open the door
    public void Interact()
    {
        if (isOpen) return; // Early return if the door is already open.
        
        // Open the door if it is unlocked
        if (!isLocked)
        {
            StartCoroutine(OpenDoor());
            return; // Early return to prevent any more code being ran  
        }
        
        // Check the players inventory for an item with the same id as the keyID
        if (playerInventory.HasItemByID(keyID))
        {
            // Open the door if an item is found
            StartCoroutine(OpenDoor());
        }
        else
        {
            Debug.Log("Door is locked, UI to show this would be handy here"); // send a message to the console for now
        } 
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
        isOpen = true; // Set isOpen to true to prevent the door being opened again
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
