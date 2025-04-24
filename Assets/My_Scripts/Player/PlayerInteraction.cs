using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius; // Radius of the physics sphere around the player

    // Method that interacts with the closest interactable object to the player within the interaction radius
    public void Interact()
    {
        // Create an array of collider components within the proximity of the player
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius);
        
        // Loop over each interactable object found
        foreach (var collider in colliders)
        {
            // Assign interactable game object to a variable
            IInteractable interactable = collider.GetComponent<IInteractable>();

            // If interactable objects are found, interact with the first one in the array and then break out of the loop
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }
}
