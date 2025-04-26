using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; // Variable assigned to the player character's animator controller
    private PlayerAnimationState currentState; // Variable used to store the current animation state 

    // Sets the animator state value to the 
    // public void SetAnimatorState(PlayerAnimationState newState)
    // {
    //     // Early return if the current state is the same as the new one
    //     if (currentState == newState) return;

    //     // Set the current state to false
    //     animator.SetBool("is" + currentState.ToString(), false);

    //     // Update the value of the new animation state to true using a switch statement
    //     switch (newState)
    //     {
    //         case PlayerAnimationState.Idle:
    //             break;
    //         case PlayerAnimationState.Running:
    //             animator.SetBool("isRunning", true);
    //             break;
    //         case PlayerAnimationState.Rolling:
    //             animator.SetBool("isRolling", true);
    //             break;
    //         case PlayerAnimationState.Dead:
    //             animator.SetBool("isDead", true);
    //             break;
    //     }

    //     // Update the current state value to equal the new state
    //     currentState = newState;
    // }

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
