using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isTyping = false; // Boolean used to track wether the player is typing or not

    [SerializeField] private TextAlert textAlert; // Variable assigned to the TextAlert script component
    [SerializeField] private TMP_InputField inputField; // Variable assigned to the input field game object in the canvas
    [SerializeField] private PlayerMovement playerMovement; // Variable assigned to the PlayerMovement script component
    [SerializeField] private EnemyStats enemy; // Variable assigned to the EnemyStats script component
    [SerializeField] private float staminaDrainRate; // Variable used to drain stamina while the typing interface is active
    private float damage; // Variable used to set the amount of damage the comment will deal
    private string[] niceWords = {"Nice!", "Almost!", "Close!", "Not bad"}; // Variable to store a few nice words, used to check against player input to determine damage
    private string targetWord; // Variable to store the current target word the player needs to match

    [Header("Script References")]
    [SerializeField] private PlayerInputs playerInputs; // Reference to the PlayerInputs script
    [SerializeField] private PlayerAnimationController playerAnimController; // Reference to the PlayerAnimationController script
    [SerializeField] private PlayerHealth playerHealth; // Reference the the PlayerHealthScript
    [SerializeField] private PlayerStamina playerStamina; // Reference the the PlayerStamina Script

    void Start()
    {
        inputField.gameObject.SetActive(false); // Disable the input field when the game starts
    }

    private void Update()
    {   
        // Deactivate the input field if the player dies (cleanup)
        if (playerHealth.playerIsDead)
        {   
            CloseInterface();
            return; // prevent any further code being ran
        }

        // Slowly drain stamina while the player is typing and ensure attack animation is playing
        if (isTyping)
        {
            playerStamina.SpendStamina(staminaDrainRate);
            playerAnimController.SetAnimatorState(PlayerAnimationState.Typing); // Set the animation to the typing loop
        }

        // If Player runs out of stamina, reset the interface for the next attack
        if (playerStamina.stamina <= 0) ResetValues();
    }

    // Method to start the attack process. Triggered by left clicking after a successful roll or parry
    public void StartVerbalAttack()
    {
        playerAnimController.SetAnimatorState(PlayerAnimationState.Typing); // Set the animation to the typing loop
        inputField.gameObject.SetActive(true); // Enable the input field
        targetWord = SetInputFieldPlaceholderValue(); // Set the placeholder text and targetWord variable
        inputField.ActivateInputField(); // Focus input field for typing
        playerMovement.allowInput = false; // Disable player movement inputs
        playerMovement.ResetMovementInputs(); // Reset vertical and horizontal movement values to remove any residual movement
        isTyping = true; // Set isTyping to true to prevent this code being spammed
        damage = textAlert.actionType == "Parry" ? 150 : 40; // Set comment damage. Parry = 150. Dodge = 40
        playerInputs.preventInputs = true; // Prevent any inputs other than return being accessed in the PlayerInputs script
        EnableSlowMotion();
    }

    // Method to end the attack process, resulting in damage dealt to the enemy. Triggered by pressing the return key
    public void EndVerbalAttack()
    {
        enemy.TakeDamage(damage, 0); // Deal damage to the enemy and 0 poise damage
        ResetValues();
    }

    // Method to set the input field placeholder value so the player has something to match their input to - returns the random string for calculating damage later
    private string SetInputFieldPlaceholderValue()
    {
        string randomWord = niceWords[Random.Range(0, niceWords.Length)]; // Randomly select word from list
        TMP_Text placeholderText = inputField.placeholder as TMP_Text; // Get the placeholder value from the input field TMP object
        placeholderText.text = randomWord; // Assign the placeholder text value to equal the random word
        return randomWord; // Return the random word value (used to calculate the damage value for later)
    }

    // Method to reset variable values for next attack
    private void ResetValues()
    {
        playerMovement.allowInput = true; // Enable player movement inputs
        isTyping = false; // set isTyping to false to allow the player to attack again after
        damage = 0; // Set the damage back to 0 ready for the next attack
        inputField.text = ""; // Set the input field text value to an empty string for the next attack
        playerInputs.preventInputs = false; // Re-enable all player inputs found in the PlayerInputs script
        playerAnimController.SetAnimatorState(PlayerAnimationState.Idle); // Set the animation to Idle
        CloseInterface(); // Call CloseInterface Method
    }

    // Method to close the interface and reset timescale value
    private void CloseInterface()
    {
        inputField.gameObject.SetActive(false); // Disable the input field
        DisableSlowMotion();
    }

    // Method to slow game time down to half speed
    private void EnableSlowMotion() => Time.timeScale = 0.3f;
    
    // Method to set game time back to default
    private void DisableSlowMotion() => Time.timeScale = 1f;
}

