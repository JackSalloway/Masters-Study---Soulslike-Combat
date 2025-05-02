using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private string storedAction; // Variable to store the action type within this script (Allows for resetting original variable before attack commences)

    [Header("Script References")]
    [SerializeField] private PlayerInputs playerInputs; // Reference to the PlayerInputs script
    [SerializeField] private PlayerAnimationController playerAnimController; // Reference to the PlayerAnimationController script
    [SerializeField] private PlayerHealth playerHealth; // Reference the the PlayerHealthScript
    [SerializeField] private PlayerStamina playerStamina; // Reference the the PlayerStamina Script
    [SerializeField] private CombatTuitionData combatTutionData; // Reference the CombatTuitionData script

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

        // Prevent stamina draining when tutorial is active
        if (combatTutionData.playerSpawnedInterface && combatTutionData.combatTutorial != null) staminaDrainRate = 0f; // prevent stamina drain while in tutorial
        else staminaDrainRate = 0.01f;

        // Unfocus input field when tutorial active
        if (combatTutionData.playerSpawnedInterface && combatTutionData.combatTutorial != null) EventSystem.current.SetSelectedGameObject(null);
        else if (inputField != null && inputField.gameObject.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            EnableSlowMotion();
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
        combatTutionData.playerSpawnedInterface = true; // If first time spawning the interface, spawn the last combat tutorial
        storedAction = textAlert.actionType; // Store the action type
        textAlert.ResetText();
        playerAnimController.SetAnimatorState(PlayerAnimationState.Typing); // Set the animation to the typing loop
        inputField.gameObject.SetActive(true); // Enable the input field
        targetWord = SetInputFieldPlaceholderValue(); // Set the placeholder text and targetWord variable
        inputField.ActivateInputField(); // Focus input field for typing
        playerMovement.allowInput = false; // Disable player movement inputs
        playerMovement.ResetMovementInputs(); // Reset vertical and horizontal movement values to remove any residual movement
        isTyping = true; // Set isTyping to true to prevent this code being spammed
        playerInputs.preventInputs = true; // Prevent any inputs other than return being accessed in the PlayerInputs script
        EnableSlowMotion();
    }

    // Method to end the attack process, resulting in damage dealt to the enemy. Triggered by pressing the return key
    public void EndVerbalAttack()
    {
        CalculateDamage(inputField.text); // Calculate how much damage should be dealt to the enemy
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

    // Calculate the amount of damage to deal based on typing accuracy
    private void CalculateDamage(string playerInput)
    {   
        // Create a loop that runs for the length of the target string
        for (int i = 0; i < targetWord.Length; i++)
        {
            // Check if the current index value exceeds the length of player input
            if (i >= playerInput.Length) break; // Break out of the loop

            char targetCharacter = targetWord[i]; // Get current character in target word (based on index of the loop)
            char inputCharacter = playerInput[i]; // Get current character in player input word (based on index of the loop)

            // Check if the two characters are the same
            if (targetCharacter == inputCharacter) damage += 10; // Add 10 damage for each correct character
        }

        // Check if the length of the player input matches the length of the target word
        if (targetWord.Length != playerInput.Length) damage *= 0.8f; // Set damage to 80% if input is not the same length

        // Check if the action the player performed was a parry
        if (textAlert.actionType == "Parry") damage *= 1.4f; // Add an extra 40% of damage if the player parried to deal damage
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

