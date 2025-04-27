using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    [SerializeField] private PlayerAnimationController playerAnimController; // Reference to the PlayerAnimationController script

    // Method to trigger the parry animation and effects. Called when the F key is pressed in the PlayerInputs script
    public void StartParry()
    {
        playerAnimController.SetAnimatorState(PlayerAnimationState.Parry);
        playerMovement.allowInput = false; // Disable player movement inputs
        playerMovement.ResetMovementInputs(); // Reset veritcal and horizontal movement variables to prevent any residual movement
    }

    // Method that sets the isParrying animator parameter to false when called. Will be called toward the end of the parry animation using keyframe events
    void EndParry()
    {
        playerMovement.allowInput = true; // Re-enable player movement inputs
        playerAnimController.SetAnimatorState(PlayerAnimationState.Idle);
    }
}
