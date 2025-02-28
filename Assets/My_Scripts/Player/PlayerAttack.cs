using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

// ----- LEGACY CODE ----- 

// public Animator animator; // Variable assigned to the player animator controller. Assigned in the inspection window
    // public PlayerStamina playerStats; // Variable assigned to PlayerStamina script. Assigned in the inspection window
    // public PlayerMovement playerMovement; // Variable assigned to PlayerMovement script. Assigned in the inspection window
    // public float weaponDamage; // Variable that specifies how much damage the weapon deals. Assigned in the inspection window
    // public float poiseDamage; // Variable that specific how much damage the weapon deals to enemy poise. Assigned in the inspection window

    // [SerializeField] private Collider weaponCollider; // Variable assigned to the collider component on the weapon. Assigned in the inspection window

    // void Start()
    // {
    //     weaponCollider.enabled = false; // Disable the collider when the script starts.
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     // Check if 3 factors are true: 
    //     // Player needs to press left mouse button
    //     // Player is not already attacking
    //     // Player is not out of stamina
    //     // If all of these are true, trigger the attack animation
    //     if (Input.GetMouseButtonDown(0) && animator.GetBool("isAttacking") == false && playerStats.stamina != 0)
    //     {
    //         animator.SetBool("isAttacking", true); // Set the animator isAttacking boolean parameter to true
    //         playerMovement.allowInput = false; // Disable movement inputs whilst attacking
    //         playerMovement.ResetMovementInputs(); // Set vertical and horizontal movement input values to 0
    //         playerStats.SpendStamina(26); // Make the attack action cost stamina
    //     }
    // }

    // // Triggers when the weapon collides with another collider
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Enemy")) // Check if the other collider is tagged as an enemy
    //     {
    //         EnemyStats enemy = other.GetComponent<EnemyStats>(); // Assign the enemies game object to a variable
    //         enemy.TakeDamage(weaponDamage, poiseDamage); // Call the damage function to deal damage to the enemy
    //     }
    // }

    // // Method to enable the weapons collider component. This will be done as an animation keyframe event towards the start of the attack
    // public void EnableWeaponCollider()
    // {
    //     weaponCollider.enabled = true; // Enable the weapon collider, allowing it to hit other game objects while swung
    // }

    // // Method to be called at the end of the attack animation using keyframe events to stop the animation.
    // void EndAttack()
    // {
    //     animator.SetBool("isAttacking", false); // Set the animator isAttacking boolean parameter to false
    //     playerMovement.allowInput = true; // Re-enable movement inputs after the attack animation is finished
    //     weaponCollider.enabled = false; // Disable the weapon collider to prevent it dealing damage without being swung
    // }
}
