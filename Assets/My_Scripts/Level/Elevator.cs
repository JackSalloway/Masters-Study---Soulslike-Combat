using UnityEngine;

public class Elevator : MonoBehaviour
{   
    [Header("Pressure Plate Variables")]
    [SerializeField] private GameObject plate; // Variable assigned to the elevator pressure plate game object
    [SerializeField] private Collider plateCollider; // Variable assigned to the box collider component above the elevator pressure plate
    [SerializeField] private bool playerOnPlate = false; // Variable to check if the player is currently on the pressure plate
    [SerializeField] private bool plateDepressed = false; // Variable to check if the pressure plate is in the up or down position
    [SerializeField] private bool plateShouldLower = false; // Variable to check if the pressure plate is in the up or down position
    [SerializeField] private bool plateShouldRaise = false; // Variable to check if the presure plate is in the up or down position
    [SerializeField] private float plateSpeed = 0.2f; // Variable to dictate how fast the pressure plate moves when lowering/raising
    [SerializeField] private Vector3 plateTargetPosition; // Variable used to store the target position of the pressure plate

    // Update is called once per frame
    private void Update()
    {
        if (plateShouldLower) LowerPlate(); // Call LowerPlate method if plateShouldLower is true

        if (plateShouldRaise && !playerOnPlate) RaisePlate(); // Call RaisePlate method if plateShouldRaise is true and the player is not currently on the plate
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
                SetPlateTargetPosition(-0.09f);
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
    private Vector3 GetCurrentPlatePosition()
    {
        return plate.transform.position;
    }

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
        float newY = Mathf.MoveTowards(currentPosition.y, plateTargetPosition.y, 0.2f * Time.deltaTime);

        // Update the plates position with the newY value to gradually move the plate
        plate.transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // Check if the target position is equal to the current position of the plate and stop it moving if so
        if (plateTargetPosition.y == plate.transform.position.y) 
        {
            plateShouldLower = false; // Plate should no longer lower
            plateShouldRaise = true; // Plate should raise back to its original position
            plateDepressed = true; // Plate is now fully depressed
            SetPlateTargetPosition(0.09f); // Set the target value for the plate to raise back up to
        }
    }

    private void RaisePlate()
    {
        // Assign the return value from GetCurrentPlatePosition to a variable
        Vector3 currentPosition = GetCurrentPlatePosition();

        // Gradually move current y position towards target y position
        float newY = Mathf.MoveTowards(currentPosition.y, plateTargetPosition.y, 0.2f * Time.deltaTime);

        // Update the plates position with the newY value to gradually move the plate
        plate.transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // Check if the target position is equal to the current position of the plate and stop it moving if so
        if (plateTargetPosition.y == plate.transform.position.y) 
        {
            plateShouldRaise = false; // Plate should no longer raise
            plateDepressed = false; // Plate is no longer depressed
        }
    }
    
    // [Header("Elevator Variables")]
    // [SerializeField] private bool elevatorAtBottom = true; // Variable used to track where the elevator is
    // [SerializeField] private float elevatorSpeed; // Variable used to set the speed the elevator moves at
    // [SerializeField] private Vector3 elevatorBottomPosition; // Variable used to store the bottom position of the elevator
    // [SerializeField] private Vector3 elevatorTopPosition; // Variable used to store the top position of the elevator
    // [SerializeField] private float lerpProgress = 0; // Variable used to track linear interpolation progress

    // [Header("Pressure Plate Variables")]
    // [SerializeField] private GameObject plate; // Variable assigned to the elevator pressure plate game object
    // [SerializeField] private Collider plateCollider; // Variable assigned to the box collider component above the elevator pressure plate
    // [SerializeField] private bool plateStoodOn = false; // Variable to check if the player is currently on the pressure plate
    // [SerializeField] private bool plateShouldMove = false; // Variable to check if the pressure plate is moving up or down
    // [SerializeField] private bool plateShouldRise = false; // Variable to check if the pressure plate should rise up
    // [SerializeField] private bool plateIsLowered = false; // Variable to check if the pressure plate has been lowered
    // [SerializeField] private Vector3 plateTargetPosition; // Variable used to store the target position of the plate

    // // Start is called once when the script is first ran
    // private void Start()
    // {
    //     // Assign the bottom position of the Elevator (default value)
    //     elevatorBottomPosition = transform.position;
    //     // Assign the top position of the elvator (default position but 23 units higher)
    //     elevatorTopPosition = new Vector3(transform.position.x, transform.position.y + 23, transform.position.z);
    // }

    // // Update is called once per frame
    // private void Update()
    // {
    //     // Check if the plate isn't lowered and if the player has stood on it, lower it if so
    //     if (!plateIsLowered && plateShouldMove) LowerPlate();

    //     // Check if the plate is lowered
    //     if (plateShouldRise && plateShouldMove) RaisePlate();
    // }

    // private void FixedUpdate()
    // {
    //     // Call the MoveElevator method when the plate is fully lowered.
    //     if (plateIsLowered) MoveElevator();
    // }

    // // OnTirggerEnter triggers when the object this code is assigned to collides with another collider
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player") && !plateShouldMove) // Check if the player stands on the plate and the plate is currently not moving
    //     {   
    //         if (plateIsLowered) return; // Early return to prevent the plate disappearing through the floor if the plate is already lowered

    //         plateStoodOn = true; // Set the plateStood on variable to true when the player stands on it
    //         plateShouldMove = true; // set the plateShouldMove variable to true when the player stands on it
    //         // Update plateTargetPosition variable with original x and z positions and new lowered y position
    //         plateTargetPosition = new Vector3(plate.transform.position.x, plate.transform.position.y - 0.09f, plate.transform.position.z);
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player")) plateStoodOn = false;
    // }

    // // Method to gradually lower the elevator pressure plate when the player stands on it
    // private void LowerPlate()
    // {
    //     // Get the current position of the plate
    //     Vector3 plateCurrentPosition = plate.transform.position;
    //     // Gradually move current y position towards target y position
    //     float newY = Mathf.MoveTowards(plateCurrentPosition.y, plateTargetPosition.y, 0.3f * Time.deltaTime);
    //     // Update the plates position with the newY value to gradually move the plate
    //     plate.transform.position = new Vector3(plateCurrentPosition.x, newY, plateCurrentPosition.z);
    //     // Check if the target position is equal to the current position of the plate and stop it moving if so
    //     if (plateTargetPosition.y == plate.transform.position.y) 
    //     {
    //         plateIsLowered = true;
    //     }
    // }

    // // Method to gradually raise the elevator pressure plate when the player isn't on it and it has stopped moving
    // private void RaisePlate()
    // {
    //     if (plateStoodOn) return; // Early return if the player is stood on the plate as it shouldn't pop back up then!

    //     // Get the current position of the plate
    //     Vector3 plateCurrentPosition = plate.transform.position;
    //     // Gradually move current y position towards target y position
    //     float newY = Mathf.MoveTowards(plateCurrentPosition.y, plateTargetPosition.y, 0.3f * Time.deltaTime);
    //     // Update the plates position with the newY value to gradually move the plate
    //     plate.transform.position = new Vector3(plateCurrentPosition.x, newY, plateCurrentPosition.z);
    //     // Check if the target position is equal to the current position of the plate and stop it moving if so
    //     if (plateTargetPosition.y == plate.transform.position.y)
    //     {
    //         plateShouldRise = false; // Pressure plate should no longer rise
    //         plateShouldMove = false; // Plate should no longer move until interacted with again

    //     }
    // }

    // // Method to move the elevator up or down depending on its current position
    // private void MoveElevator()
    // {
    //     // Check if the elevator is at the bottom or the top of the shaft
    //     if (elevatorAtBottom) 
    //     {   
    //         // Increment linear interpolation progress
    //         lerpProgress += Time.deltaTime * elevatorSpeed; 
    //         // Linearly interpolate between bottom and top positions
    //         transform.position = Vector3.Lerp(elevatorBottomPosition, elevatorTopPosition, lerpProgress);

    //         // Update elevator position and relevant booleans when elevator reaches the top
    //         if (lerpProgress >= 1) 
    //         {   
    //             transform.position = elevatorTopPosition; // Ensure the elevator is at the top positon
    //             elevatorAtBottom = false; // Elevator is now at the top
    //             plateIsLowered = false;
    //             plateShouldRise = true;
                
    //             // Update plateTargetPosition variable with original x and z positions and new lowered y position
    //             plateTargetPosition = new Vector3(plate.transform.position.x, plate.transform.position.y + 0.09f, plate.transform.position.z);
    //         }
    //     } else
    //     {
    //         // Increment linear interpolation progress
    //         lerpProgress += Time.deltaTime * elevatorSpeed; 
    //         // Linearly interpolate between bottom and top positions
    //         transform.position = Vector3.Lerp(elevatorTopPosition, elevatorBottomPosition, lerpProgress);

    //         // Update elevator position and relevant booleans when elevator reaches the bottom
    //         if (lerpProgress >= 1) 
    //         {   
    //             transform.position = elevatorTopPosition; // Ensure the elevator is at the top positon
    //             elevatorAtBottom = true; // Elevator is now at the bottom
    //             plateShouldRise = true;
                
    //             // Update plateTargetPosition variable with original x and z positions and new lowered y position
    //             plateTargetPosition = new Vector3(plate.transform.position.x, plate.transform.position.y + 0.09f, plate.transform.position.z);
    //         }
    //     }
    // }
}
