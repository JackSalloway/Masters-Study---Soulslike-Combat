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

