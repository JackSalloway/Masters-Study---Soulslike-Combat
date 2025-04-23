using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {   
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

        // ------------------------------------------------
        // MOUSE INPUT - Handles player camera manipulation
        mouseX = Input.GetAxis("Mouse X"); // Get X axis input
        mouseY = Input.GetAxis("Mouse Y"); // Get Y axis input
        playerCamera.SetMouseInput(new Vector2(mouseX, mouseY)); // Call SetMouseInput method in PlayerCamera script

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

        // -------------------------------------------------
        // RETURN (ENTER) KEY - Handles ending player attack
        if (Input.GetKeyDown(KeyCode.Return) && playerAttack.isTyping == true) playerAttack.EndVerbalAttack();
    }
}
