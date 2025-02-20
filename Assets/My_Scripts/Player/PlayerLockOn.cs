using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    public PlayerCamera playerCamera; // Variable assigned to the player camera script. Assigned in the inspection window

    // Update is called once per frame
    void Update()
    {
        // Check if the middle mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse2)) 
        {
            playerCamera.lockedOn = !playerCamera.lockedOn; // Toggle the vale of the lockedOn variable
        }
    }
}
