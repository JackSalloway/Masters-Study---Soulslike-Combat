using UnityEngine;

public class ToggleCollider : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider; // Create weaponCollider variable to be assigned in the inspection window

    // This code is required to prevent the weapon from always having a collider active.
    // Always having a collider means that the weapon will deal damage even if an attack animation is not playing (not the intended effect)

    // Method to enable the weapons collider component. This will be done as an animation keyframe event towards the start of the attack
    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    // Method to disable the weapons collider component. This will be done as an animation keyframe event towards the end of the attack
    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }
}
