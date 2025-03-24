using UnityEngine;

public class Elevator : MonoBehaviour
{   
    [Header("Elevator Variables")]
    [SerializeField] private bool elevatorAtBottom = true; // Variable used to track where the elevator is
    [SerializeField] private float elevatorSpeed; // Variable used to set the speed the elevator moves at
    [SerializeField] private Vector3 elevatorBottomPosition; // Variable used to store the bottom position of the elevator
    [SerializeField] private Vector3 elevatorTopPosition; // Variable used to store the top position of the elevator
    [SerializeField] private float lerpProgress = 0; // Variable used to track linear interpolation progress

    [Header("Pressure Plate Variables")]
    [SerializeField] private GameObject plate; // Variable assigned to the elevator pressure plate game object
    [SerializeField] private Collider plateCollider; // Variable assigned to the box collider component above the elevator pressure plate
    [SerializeField] private bool plateStoodOn = false; // Variable to check if the player is currently on the pressure plate
    [SerializeField] private bool plateShouldMove = false; // Variable to check if the pressure plate is moving up or down
    [SerializeField] private bool plateIsLowered = false; // Variable to check if the pressure plate has been lowered
    [SerializeField] private Vector3 plateTargetPosition; // Variable used to store the target position of the plate

    // Start is called once when the script is first ran
    private void Start()
    {
        // Assign the bottom position of the Elevator (default value)
        elevatorBottomPosition = transform.position;
        // Assign the top position of the elvator (default position but 23 units higher)
        elevatorTopPosition = new Vector3(transform.position.x, transform.position.y + 23, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the plate isn't lowered and if the player has stood on it, lower it if so
        if (!plateIsLowered && plateShouldMove) LowerPlate();
    }

    // OnTirggerEnter triggers when the object this code is assigned to collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !plateShouldMove) // Check if the player stands on the plate and the plate is currently not moving
        {   
            if (plateIsLowered) return; // Early return to prevent the plate disappearing through the floor if the plate is already lowered

            plateStoodOn = true; // Set the plateStood on variable to true when the player stands on it
            plateShouldMove = true; // set the plateShouldMove variable to true when the player stands on it
            // Update plateTargetPosition variable with original x and z positions and new lowered y position
            plateTargetPosition = new Vector3(plate.transform.position.x, plate.transform.position.y - 0.09f, plate.transform.position.z);
        }
    }

    // Method to gradually lower the elevator pressure plate when the player stands on it
    private void LowerPlate()
    {
        // Get the current position of the plate
        Vector3 plateCurrentPosition = plate.transform.position;
        // Gradually move current y position towards target y position
        float newY = Mathf.MoveTowards(plateCurrentPosition.y, plateTargetPosition.y, 0.3f * Time.deltaTime);
        // Update the plates position with the newY value to gradually move the plate
        plate.transform.position = new Vector3(plateCurrentPosition.x, newY, plateCurrentPosition.z);
        // Check if the target position is equal to the current position of the plate and stop it moving if so
        if (plateTargetPosition.y == plate.transform.position.y) 
        {
            plateShouldMove = false;
            plateIsLowered = true;
        }
    }

    // Method to move the elevator up or down depending on its current position
    private void MoveElevator()
    {
        // Check if the elevator is at the bottom or the top of the shaft
        if (elevatorAtBottom) 
        {   
            // Increment linear interpolation progress
            lerpProgress += Time.deltaTime * elevatorSpeed; 
            // Linearly interpolate between bottom and top positions
            transform.position = Vector3.Lerp(elevatorBottomPosition, elevatorTopPosition, lerpProgress);

            // Update elevator position and relevant booleans when elevator reaches the top
            if (lerpProgress >= 1) 
            {   
                transform.position = elevatorTopPosition; // Ensure the elevator is at the top positon
                plateIsLowered = false; // Trigger the plate to rise again as the elevator has reached its destination
                elevatorAtBottom = false; // Elevator is now at the top
            }
        }
    }
}
