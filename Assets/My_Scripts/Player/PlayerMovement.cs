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

    [SerializeField] private float verticalMovement; // Variable for storing backwards and forward movement values
    [SerializeField] private float horizontalMovement; // Variable for storing left and right movement values
    [SerializeField] private float stepSmoothing; // Variable used to smooth the y movement when stepping up small inclines
    [SerializeField] private GameObject lowerRaycast; // Variable assigned to a GameObject located in the player characters foot
    [SerializeField] private GameObject upperRaycast; // Variable assigned to a GameObject located in the player characters shin

    private Rigidbody rb; // Variable assigned to the player characters rigid body component

    [Header("Script References")]
    [SerializeField] private PlayerStamina playerStats; // Variable assigned to PlayerStamina script. Will be assigned in the inspection window
    [SerializeField] private PlayerHealth playerHealth; // Variable assigned to the PlayerHealth script. Assigned in the inspection window.
    [SerializeField] private PlayerCamera playerCamera; // Variable assigned to the Player Camera script. Assigned in the inspection window

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Assign rb variable to the player characters rigid body component when the script starts
    }

    public void MovePlayer(float verticalInput, float horizontalInput)
    {
        // Check if the player is dead and early return to prevent movement
        if (animator.GetBool("isDead") == true) return;

        // Get both the vertical and horizontal input values if the allowInput variable is set to true.
        if (allowInput == true) 
        {
            verticalMovement = verticalInput * movementSpeed * Time.deltaTime; // W and S keys.
            horizontalMovement = horizontalInput * movementSpeed * Time.deltaTime; // A and D keys.
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
        Vector3 forwardRelativeVerticalInput = verticalMovement * forward;
        Vector3 rightRelativeHorizonatalInput = horizontalMovement * right;

        // Create camera relative movement
        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizonatalInput;

        // Move player forward while rolling and prevent any input while rolling
        if (animator.GetBool("isRolling") == true) {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.Self); // Move the player forward 1 unit on its local space
            return; // Early return to prevent any further inputs until the roll animation is over
        };

        // Update animation variable value
        if (verticalMovement != 0 || horizontalMovement != 0) {
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
        if (verticalMovement !=0 || horizontalMovement != 0)
        {   
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeMovement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Update is called every 0.02 seconds (50 times per second)
    void FixedUpdate()
    {
        ClimbStep();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player walks over an elevator platform
        if (other.CompareTag("ElevatorPlatform"))
        {   
            // Set the elevator platform to be the parent of the player character model so they move together
            transform.SetParent(other.transform); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player walks off an elevator platform
        if (other.CompareTag("ElevatorPlatform"))
        {   
            // Set the parent of the player character to null when the leaves the elevator platform
            transform.SetParent(null); 
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
        verticalMovement = 0; // Reset vertical input value to prevent any residual movement
        horizontalMovement = 0; // Reset horizontal input value to prevent any residual movement
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

    // Method used to allow the player to smoothly climb up vertical game objects (small inclines)
    private void ClimbStep()
    {
        // Send out a raycast from the player characters foot to see if they are walking into a game object
        if (Physics.Raycast(lowerRaycast.transform.position, transform.TransformDirection(Vector3.forward), 0.1f))
        {
            // Send out a raycast from the player characters shin to see if there is empty space for them to stand on
            if (!Physics.Raycast(upperRaycast.transform.position, transform.TransformDirection(Vector3.forward), 0.2f))
            {
                // Adjust the player characters y position by the stepSmoothing value
                rb.transform.position -= new Vector3(0f, -stepSmoothing, 0f);
            }
        }
    }
}