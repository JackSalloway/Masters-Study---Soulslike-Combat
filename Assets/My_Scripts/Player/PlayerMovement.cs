using UnityEngine;


public class PlayerMovement : MonoBehaviour
{    
    public Animator animator; 
    public PlayerStamina playerStats; // Variable assigned to PlayerStamina script. Will be assigned in the inspection window
    public PlayerHealth playerHealth; // Variable assigned to the PlayerHealth script. Assigned in the inspection window.
    public float movementSpeed; // Variable used to control how fast the player is. Assigned in the inspection window
    public bool invulnerabilityFramesActive = false; // Variable used to tell if the player should take damage when being hit.
    public Transform cameraTransform; // Variable assigned to the main cameras transform component, used to update camera position/rotation
    public PlayerCamera playerCamera; // Variable assigned to the Player Camera script. Assigned in the inspection window
    public float rotationSpeed; // Vairiable used to control how fast the player model rotates. Assigned in the inspection window
    public bool allowInput = true; // Variable used to prevent input when the player is rolling

    [SerializeField] private float verticalInput; // Variable for storing the input values of the W and S keys
    [SerializeField] private float horizontalInput; // Variable for storing the input values of the A and D keys

    // Update is called once per frame
    void Update()
    {
        // Check if the player is dead and early return to prevent movement
        if (animator.GetBool("isDead") == true) return;

        // Get both the vertical and horizontal input values if the allowInput variable is set to true.
        if (allowInput == true) 
        {
            verticalInput = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime; // W and S keys.
            horizontalInput = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime; // A and D keys.
        }
        
        // Get camera-relative direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten vertical direction to prevent flying
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Create direction-relative input vectors
        Vector3 forwardRelativeVerticalInput = verticalInput * forward;
        Vector3 rightRelativeHorizonatalInput = horizontalInput * right;

        // Create camera relative movement
        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizonatalInput;

        // Move player forward while rolling and prevent any input while rolling
        if (animator.GetBool("isRolling") == true) {
            // Move the transform component towards forwards locally
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.Self);
            return; // Early return to prevent any further inputs until the roll animation is over
        };

        // Update animation variable value
        if (verticalInput != 0 || horizontalInput != 0) {
            SetAnimatorParameter("isRunning", true);
            transform.Translate(cameraRelativeMovement, Space.World);

            // Check if the space button has been pressed & the player has stamina available. Trigger a roll animation if so
            if (Input.GetKeyDown(KeyCode.Space) && playerStats.stamina > 0) {
                playerStats.SpendStamina(25);
                SetAnimatorParameter("isRolling", true);
            }
        } else SetAnimatorParameter("isRunning", false);     

        // If the player is locked on to the enemy, rotate them to look at the enemy
        if (playerCamera.lockedOn == true)
        {
            Quaternion targetRotation = Quaternion.LookRotation(forward).normalized; // Variable storing the rotation the camera is facing
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Rotate the player
            return; // Early return to prevent any more player rotation
        }

        // Rotate the character to reflect the direction they are moving provided they are moving and not rolling
        if (verticalInput !=0 || horizontalInput != 0)
        {   
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeMovement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Method to set animator controller parameters depending on supplied arguments
    void SetAnimatorParameter(string paramName, bool value)
    {
        animator.SetBool(paramName, value);
    }

    // Method used to set current player movement values to 0. Used during certain animations
    public void ResetMovementInputs()
    {
        verticalInput = 0; // Reset vertical input value to prevent any residual movement
        horizontalInput = 0; // Reset horizontal input value to prevent any residual movement
    }

    // Method used to set the isRolling parameter to true and disable player inputs. This will be called at the start of the roll animation using an event
    void StartRoll()
    {   
        allowInput = false; // Disable player inputs
        ResetMovementInputs();
        invulnerabilityFramesActive = true; // Prevent the player from taking damage
    }

    // Method to be called when the roll animation ends.
    void StopRoll() 
    {
        allowInput = true; // Re-enable player inputs
        invulnerabilityFramesActive = false; // Make it so the player can take damage again
        animator.SetBool("isRolling", false); // End the rolling animation 
    } 
}














// Public Variables
    // public Animator animator; 
    // public PlayerStamina playerStats; // Variable assigned to PlayerStamina script. Will be assigned in the inspection window
    // public PlayerHealth playerHealth; // Variable assigned to the PlayerHealth script. Assigned in the inspection window.
    // public float movementSpeed; // Variable used to control how fast the player is. Assigned in the inspection window
    // public bool invulnerabilityFramesActive = false; // Variable used to tell if the player should take damage when being hit.
    // public Transform cameraTransform; // Variable assigned to the main cameras transform component, used to update camera position/rotation
    // public PlayerCamera playerCamera; // Variable assigned to the Player Camera script. Assigned in the inspection window
    // public float rotationSpeed; // Vairiable used to control how fast the player model rotates. Assigned in the inspection window
    // public bool allowInput = true; // Variable used to prevent input when the player is rolling

    // // Private Variables
    // [SerializeField] private float verticalInput; // Variable for storing the input values of the W and S keys
    // [SerializeField] private float horizontalInput; // Variable for storing the input values of the A and D keys

























// Public Variables
    // public Animator animator; // Variable assigned to the player character animator. Assigned in the inspection window
    // public float movementSpeed; // Variable used to affect how fast the player moves. Assigned in the inspection window 
    
    // // Update is called once per frame
    // void Update()
    // {
    //     // Get both the vertical and horizontal input values. Both GetAxis methods  return a float in the range of -1 to 1
    //     float verticalInput = Input.GetAxis("vertical") * movementSpeed * Time.deltaTime; // W and S keys
    //     float horizontalInput = Input.GetAxis("horizontal") * movementSpeed * Time.deltaTime; // A and D keys

    //     // Check if the W, A, S, or D keys are being pressed. Transition to the running animation if they are, idle animation if not
    //     if (verticalInput != 0 || horizontalInput != 0) 
    //     {
    //         animator.SetBool("isRunning", true); // Set the animator isRunning parameter to true
    //         // Move mesh using input values. 
    //         transform.Translate(horizontalInput, 0, verticalInput); // Method declaration example: Translate(float x, float y, float z)
    //     }
    //     else animator.SetBool("isRunning", false); // Set the animator isRunning parameter to false        
    // }  