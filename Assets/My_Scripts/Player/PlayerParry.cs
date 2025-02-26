using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public Animator animator; // Variable assigned to the player animator controller. Assigned in the inspection window

    // Update is called once per frame
    void Update()
    {
        // Set isParrying animator parameter to true when the player presses the F key
        if (Input.GetKeyDown(KeyCode.F)) animator.SetBool("isParrying", true);
    }

    // Method that sets the isParrying animator parameter to false when called. Will be called toward the end of the parry animation using keyframe events
    void EndParry() => animator.SetBool("IsParrying", false);
}
