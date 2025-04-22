using UnityEngine;

public class Elevator : MonoBehaviour
{   
    [Header("Pressure Plate Variables")]
    [SerializeField] private GameObject plate; // Variable assigned to the elevator pressure plate game object
    [SerializeField] private Collider plateCollider; // Variable assigned to the box collider component above the elevator pressure plate
    [SerializeField] private bool playerOnPlate = false; // Variable to check if the player is currently on the pressure plate
    [SerializeField] private bool plateDepressed = false; // Variable to check if the pressure plate is in the up or down position
    [SerializeField] private bool plateShouldLower = false; // Variable to check if the pressure plate is currently lowering
    [SerializeField] private bool plateShouldRaise = false; // Variable to check if the presure plate is currently raising
    [SerializeField] private float plateSpeed = 0.2f; // Variable to dictate how fast the pressure plate moves when lowering/raising
    [SerializeField] private Vector3 plateTargetPosition; // Variable used to store the target position of the pressure plate

    [Header("Elevator Variables")]
    [SerializeField] private bool elevatorAtBottom = true; // Variable used to track where the elevator is (bottom is default)
    [SerializeField] private bool elevatorShouldMove = false; // Variable used to track if the elevator is currently moving
    [SerializeField] private float elevatorSpeed = 0.1f; // Variable used to set the speed the elevator moves at
    [SerializeField] private Vector3 elevatorBottomPosition; // Variable used to store the bottom position of the elevator
    [SerializeField] private Vector3 elevatorTopPosition; // Variable used to store the top position of the elevator
    [SerializeField] private float lerpProgress = 0; // Variable used to track linear interpolation progress of elevator movement

    // Start is called once when the script is first ran
    private void Start()
    {
        // Assign the bottom position of the Elevator (default value)
        elevatorBottomPosition = transform.position;
        // Assign the top position of the elvator (default position but 23 units higher)
        elevatorTopPosition = new Vector3(transform.position.x, transform.position.y + 24, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if (plateShouldLower) LowerPlate(); // Call LowerPlate method if plateShouldLower is true

        if (plateShouldRaise && !playerOnPlate) RaisePlate(); // Call RaisePlate method if plateShouldRaise is true and the player is not currently on the plate
    }

    private void FixedUpdate()
    {   
        // Check if the elevator should be moving or not
        if (elevatorShouldMove) 
        {   
            // Send the elevator up or down depending on its current position
            if (elevatorAtBottom == true) RaiseElevator();
            else LowerElevator();
        }
    }

    // OnTirggerEnter is called when two game objects with a collider component touch or overlap 
    private void OnTriggerEnter(Collider other)
    {
        // Check the tag is tied to the player character
        if (other.CompareTag("Player"))
        {   
            playerOnPlate = true; // Set playerOnPlate variable to true as the player is currently on the plate

            // Check if the pressure plate is not lowered already or in the middle of lowering
            if (!plateDepressed && !plateShouldLower)
            {
                plateShouldLower = true; // Set the plateShouldLower variable to true to begin lowering the plate
                SetPlateTargetPosition(-0.09f); // Update plate target position
            }
        }
    }

    // OnTriggerExit is called when a game object with a collider stops touching
    private void OnTriggerExit(Collider other)
    {
        // Check the tag is tied to the player character
        if (other.CompareTag("Player"))
        {
            playerOnPlate = false; // Set playerOnPlate variable to false as they are no longer on the plate
        }
    }

    // Method to return the current position of the plate
    private Vector3 GetCurrentPlatePosition() => plate.transform.position;

    // Method to set the target position of the pressure plate based the float parament yDifference
    private void SetPlateTargetPosition(float yDifference)
    {
        plateTargetPosition = new Vector3(plate.transform.position.x, plate.transform.position.y + yDifference, plate.transform.position.z);
    }

    private void LowerPlate()
    {
        // Assign the return value from GetCurrentPlatePosition to a variable
        Vector3 currentPosition = GetCurrentPlatePosition();

        // Gradually move current y position towards target y position
        float newY = Mathf.MoveTowards(currentPosition.y, plateTargetPosition.y, plateSpeed * Time.fixedDeltaTime);

        // Update the plates position with the newY value to gradually move the plate
        plate.transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // Check if the target position is equal to the current position of the plate and stop it moving if so
        if (plateTargetPosition.y >= plate.transform.position.y) 
        {
            plateShouldLower = false; // Plate should no longer lower
            plateDepressed = true; // Plate is now fully depressed
            elevatorShouldMove = true; // Pressure plate is depressed so the elevator should start moving
        }
    }

    private void RaisePlate()
    {
        // Assign the return value from GetCurrentPlatePosition to a variable
        Vector3 currentPosition = GetCurrentPlatePosition();

        // Gradually move current y position towards target y position
        float newY = Mathf.MoveTowards(currentPosition.y, plateTargetPosition.y, plateSpeed * Time.fixedDeltaTime);

        // Update the plates position with the newY value to gradually move the plate
        plate.transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // Check if the target position is equal to the current position of the plate and stop it moving if so
        if (plateTargetPosition.y <= plate.transform.position.y) 
        {
            plateShouldRaise = false; // Plate should no longer raise
            plateDepressed = false; // Plate is no longer depressed
        }
    }

    private void RaiseElevator()
    {
        // Increment linear interpolation progress per call
        lerpProgress += Time.fixedDeltaTime * elevatorSpeed;

        // Linearly interpolate between bottom and top positions
        transform.position = Vector3.Lerp(elevatorBottomPosition, elevatorTopPosition, lerpProgress);

        if (lerpProgress >= 1) 
            {   
                transform.position = elevatorTopPosition; // Ensure the elevator is at the top positon
                elevatorAtBottom = false; // Elevator is now at the top
                elevatorShouldMove = false; // Elevator should no longer be moving
                lerpProgress = 0; // Reset linear interpolation progress for next elevator movement
                SetPlateTargetPosition(0.09f); // Set the target value for the plate to raise back up to
                plateShouldRaise = true; // Plate should raise back to its original position now the elevator is finished moving
            }
    }

    private void LowerElevator()
    {
        // Increment linear interpolation progress per call
        lerpProgress += Time.fixedDeltaTime * elevatorSpeed;

        // Linearly interpolate between bottom and top positions
        transform.position = Vector3.Lerp(elevatorTopPosition, elevatorBottomPosition, lerpProgress);

        if (lerpProgress >= 1) 
            {   
                transform.position = elevatorBottomPosition; // Ensure the elevator is at the bottom positon
                elevatorAtBottom = true; // Elevator is now at the bottom
                elevatorShouldMove = false; // Elevator should no longer be moving
                lerpProgress = 0; // Reset linear interpolation progress for next elevator movement
                SetPlateTargetPosition(0.09f); // Set the target value for the plate to raise back up to
                plateShouldRaise = true; // Plate should raise back to its original position now the elevator is finished moving
            }
    }
}