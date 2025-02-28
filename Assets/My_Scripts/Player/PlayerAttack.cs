using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private TextAlert textAlert; // Variable assigned to the TextAlert script component
    [SerializeField] private TMP_InputField inputField; // Variable assigned to the input field game object in the canvas
    [SerializeField] private PlayerMovement playerMovement; // Variable assigned to the PlayerMovement script component
    [SerializeField] private EnemyStats enemy; // Variable assigned to the EnemyStats script component
    private bool isTyping = false; // Boolean used to track wether the player is typing or not
    private float damage; // Variable used to set the amount of damage the comment will deal

    void Start()
    {
        inputField.gameObject.SetActive(false); // Disable the input field when the game starts
    }

    void Update()
    {   
        // Check if the player presses the enter key while they are typing
        if (Input.GetKeyDown(KeyCode.Return) && isTyping == true)
        {
            enemy.TakeDamage(damage, 0); // Deal damage to the enemy and 0 poise damage
            inputField.gameObject.SetActive(false); // Disable the input field
            playerMovement.allowInput = true; // Enable player movement inputs
            isTyping = false; // set isTyping to false to allow the player to attack again after
            damage = 0; // Set the damage back to 0 ready for the next attack
            inputField.text = ""; // Set the input field text value to an empty string for the next attack
        }

        if (textAlert.actionType == "") return; // Early return if the player has yet to perform a successful dodge or parry

        // Check if the player clicks the left mouse button down and is not already typing in the input field
        if (Input.GetMouseButtonDown(0) && isTyping == false)
        {
            inputField.gameObject.SetActive(true); // Enable the input field
            inputField.Select(); // Focus the input field
            playerMovement.allowInput = false; // Disable player movement inputs
            playerMovement.ResetMovementInputs(); // Reset vertical and horizontal movement values to remove any residual movement
            isTyping = true; // Set isTyping to true to prevent this code being spammed
            damage = textAlert.actionType == "Parry" ? 150 : 40; // Set comment damage. Parry = 150. Dodge = 40
        }
    }

    
}

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

