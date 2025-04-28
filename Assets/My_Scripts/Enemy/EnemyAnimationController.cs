using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator; // Variable assigned to the player character's animator controller
    private PlayerAnimationState currentState; // Variable used to store the current animation state 

    public void SetAnimatorState(PlayerAnimationState newState)
    {
        // Early return if the current state is the same as the new one
        if (currentState == newState) return;

        // Update the animator playerState parameter to the new value
        animator.SetInteger("playerState", (int)newState);

        // Update value of the currentState variable
        currentState = newState;
    }

    // Return whether the current state matches the provided state
    public bool GetAnimatorStateValue(PlayerAnimationState state)
    {
        return animator.GetInteger("playerState") == (int)state;
    }
}
