using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public bool preventInputs = false; // Variable used to prevent inputs when the player is typing (attacking)

    [Header("Mouse Input Values")]
    private float mouseX; // Variable to store mouse X axis movement
    private float mouseY; // Variable to store mouse Y axis movement

    [Header("Movement Input Values")]
    private float verticalInput; // Variable to store vertical inputs (W & S keys)
    private float horizontalInput; // Variable to store horizontal inputs (A & D keys)

    [Header("Tutorial Values")]
    [SerializeField] private float tabHoldDuration; // Variable used to store how long the player needs to hold the TAB key to toggle tutorials
    [SerializeField] private float tabHoldTimer; // Variable used to track how long the TAB key is held for

    [Header("Script References")]
    [SerializeField] private TuitionData tutorialController; // Reference to the TuitionData script
    [SerializeField] private PlayerCamera playerCamera; // Reference to the PlayerCamera script
    [SerializeField] private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    [SerializeField] private PlayerParry playerParry; // Reference to the PlayerParry script
    [SerializeField] private PlayerAttack playerAttack; // Reference to the PlayerAttack script
    [SerializeField] private TextAlert textAlert; // Reference to the TextAlert script
    [SerializeField] private PlayerInteraction playerInteraction; // Reference to the PlayerInteraction script
    [SerializeField] private PlayerEnemyDetection playerEnemyDetection; // Reference to the PlayerEnemyDetection script
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    // Update is called once per frame
    void Update()
    {   
        // Prevent all inputs if the player is dead
        if (playerHealth.playerIsDead) return;

        // -------------------------------------------------
        // RETURN (ENTER) KEY - Handles ending player attack
        if (Input.GetKeyDown(KeyCode.Return) && playerAttack.isTyping == true) playerAttack.EndVerbalAttack();

        // Prevent any input listed below if the player is typing
        if (preventInputs) return;

        // --------------------------------------------------------------------
        // TAB KEY - Handles toggling tutorial between small and large versions
        if (Input.GetKey(KeyCode.Tab))
        {
            if (tutorialController.tutorialActive == false) return; // Early return if no tutorials are currently active

            tabHoldTimer += Time.deltaTime; // Increment timer

            // Toggle tutorials active property when the TAB key is held for long enough
            if (tabHoldTimer >= tabHoldDuration)
            {
                tutorialController.ToggleActiveTutorial(); // Toggle tutorials active properties 
                tabHoldTimer = 0; // Reset timer value to 0
            }
        }
        
        // Check if the player releases the TAB key and reset the tabHoldTimer variable to 0 when they do
        if (Input.GetKeyUp(KeyCode.Tab)) tabHoldTimer = 0;

        // ------------------------------
        // E KEY - Handles multiple things:
        // 1. Closing tutorials without leaving the bounds
        // 2. Interacting with other game objects (doors, items, levers, etc...)
        playerInteraction.UpdatePrompt(); // Always check for interactables

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if a tutorial is open and delete them if so
            if (tutorialController.tutorialActive)
            {
                tutorialController.DeleteTutorials();
                return; // Early return to prevent any more code being ran.
            }

            // Attempt to interact with a game object within range of the player's interaction range
            playerInteraction.Interact();
        }

        // ------------------------------------------------
        // MOUSE INPUT - Handles player camera manipulation
        mouseX = Input.GetAxis("Mouse X"); // Get X axis input
        mouseY = Input.GetAxis("Mouse Y"); // Get Y axis input
        playerCamera.SetMouseInput(new Vector2(mouseX, mouseY)); // Call SetMouseInput method in PlayerCamera script

        //
        // MIDDLE MOUSE BUTTON - Handles locking on to enemies
         if (Input.GetKeyDown(KeyCode.Mouse2)) 
        {   
            if (playerEnemyDetection.detectedEnemies.Count == 0) return; // Early return if no enemies detected
            playerCamera.lockedOn = !playerCamera.lockedOn; // Toggle the value of the lockedOn variable
        }

        // -------------------------------------------
        // W, A, S, D KEYS - Handles player movement
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        playerMovement.MovePlayer(verticalInput, horizontalInput);

        //--------------------------------
        // F KEY - Handles player parrying
        if (Input.GetKey(KeyCode.F)) playerParry.StartParry();

        // ---------------------------------------------------
        // MOUSE BUTTON 0 (LEFT CLICK) - Handles starting player attack
        if (Input.GetMouseButtonDown(0) && playerAttack.isTyping == false)
        {
            if (textAlert.actionType == "") return; // Early return if the player has yet to perform a successful dodge or parry
            playerAttack.StartVerbalAttack();
        }
    }
}
