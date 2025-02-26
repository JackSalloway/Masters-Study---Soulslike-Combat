using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public Animator animator; // Variable assigned to the player animator controller. Assigned in the inspection window
    
    [SerializeField] private PlayerMovement playerMovement; // Variable assigned to the PlayerMovement script component on the player game object.

    // Update is called once per frame
    void Update()
    {
        // Set isParrying animator parameter to true when the player presses the F key
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            animator.SetBool("isParrying", true); // Set the isParrying animator parameter to true
            playerMovement.allowInput = false; // Disable player movement inputs
            playerMovement.ResetMovementInputs(); // Reset veritcal and horizontal movement variables to prevent any residual movement
        }
    }

    // Method that sets the isParrying animator parameter to false when called. Will be called toward the end of the parry animation using keyframe events
    void EndParry()
    {
        playerMovement.allowInput = true; // Re-enable player movement inputs
        animator.SetBool("isParrying", false); // Set the isParrying animator parameter to false
    }
}
