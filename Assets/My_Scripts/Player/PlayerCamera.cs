using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Public Variables
    public GameObject playerCharacter; // Variable to be assigned to the player characters model in the inspection window
    public bool lockedOn = false; // Variable to decide if the camera should lock on to the enemy or not
    public GameObject enemy; // Variable assigned to the enemy game object. Assigned in the inspection window

    // Private Variables
    [SerializeField] private float mouseSensitivity; // Variable for storing how sensitive the mouse movement will be
    private float rotationSmoothTime = 0.12f; // Variable used for smoothing camera rotation
    private float yaw = 0f; // Variable to store the value of cameras yaw rotation
    private float pitch = 0f; // Variable to store the value of the cameras pitch rotation
    private float minYAngle = -40f; // Variable to prevent the user from rotating the camera too far down
    private float maxYAngle = 40f; // Variable to prevent the user from rotating the camera too far up
    private Vector3 currentRotation; // Variable to store the current rotation of the camera
    private Vector3 rotationSmoothVelocity; // Variable to be used to smooth the rotation of the camera, uses the ref keyword

    // Start will be called once when the script is ran
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor when the game is launched
        Cursor.visible = false; // Hide the cursor when the game is launched

        // Initialize yaw and pitch to the current camera values when the game is launched
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockedOn == true)
        {
            transform.LookAt(enemy.transform.position); // Target the enemy's position
            Vector3 testOffset = new Vector3(0, 2, -3); // Original position of the camera related to the player game object
            transform.position = playerCharacter.transform.position + testOffset; // Update camera position as the player moves
            return; // Early return to prevent camera mouse movement
        }


        // Get mouse inputs for both x and y mouse axes
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Update yaw and pitch rotations
        yaw += mouseX; // Due to how rotations work, mouseX will be used for the yaw value which will rotate the Y axis
        pitch -= mouseY; // Due to how rotations work, mouseY will be used for the pitch value which will rotate the X axis
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle); // set the min and max values for pitch rotation

        // Create a new Vector3 struct with the updated rotation values
        Vector3 targetRotation = new Vector3(pitch, yaw, 0);
        
        // Smooth the rotation
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Create rotation as a quaternion euler
        Quaternion rotation = Quaternion.Euler(currentRotation);

        // Keep the camera offset the same
        Vector3 offset = rotation * new Vector3(0, 2, -3);
            
        // Set the position and rotation values of the cameras transform component
        transform.SetPositionAndRotation(playerCharacter.transform.position + offset, Quaternion.Euler(currentRotation.x, currentRotation.y, 0));        
    }
}
