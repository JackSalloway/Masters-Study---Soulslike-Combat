using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius; // Radius of the physics sphere around the player
    [SerializeField] private GameObject promptUIPrefab; // Variable assigned to the Interaction_Prompt prefab
    [SerializeField] private Transform canvasTransform; // Variable assigned to the main Canvas game objects transform component
    [SerializeField] private GameObject currentPromptUI; // Variable to store instantiated Interaction_Prompt game object
    [SerializeField] private IInteractable currentInteractable; // Variable to store a reference to the closest interactable interface

    // Method that interacts with the closest interactable object to the player within the interaction radius
    public void Interact()
    {
        // Interact with an object if there is one within range
        if (currentInteractable != null) currentInteractable.Interact();
    }

    // Method to find the closest interactable and instantiate a UI prompt if one exists
    public void UpdatePrompt()
    {
           // Create an array of collider components within the proximity of the player
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius);
        
        // Create variables used to find and store the nearest interactable object
        IInteractable nearestInteractable = null;
        float closestDistance = float.MaxValue; // Little overkill, but will work with any interaction radius if changed

        // Loop over each interactable object found
        foreach (var collider in colliders)
        {   
            // Assign interactable game object to a variable
            IInteractable interactable = collider.GetComponent<IInteractable>();

            // If interactable objects are found, test against the previous closestDistance value to find if a new interactable is closer
            if (interactable != null)
            {   
                // Measure the distance between the player character and the interactable game object
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                // If distance is less than closestDistance, closestDistance = distance
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestInteractable = interactable;
                }
            }
        }

        // Instantiate an Interaction_Prompt game object if an interactable is close, ensure there isn't one active if not
        if (nearestInteractable !=null)
        {
            // Check that the nearest interactable isn't already the current interactable stored
            if (nearestInteractable != currentInteractable)
            {
                currentInteractable = nearestInteractable; // Set the value of the current stored interactable to equal the nearest

                // Delete the current Interaction_Prompt game object if one exists
                if (currentPromptUI != null) Destroy(currentPromptUI);

                // Instantiate new Interaction_Prompt game object as a child of the main Canvas game object
                currentPromptUI = Instantiate(promptUIPrefab, canvasTransform);
                // Find and assign the action text to a variable
                Transform actionTextTransform = currentPromptUI.transform.Find("Action_Text");
                // Get the action text's TextMeshProUGUI component and update the text to fit the interactable object
                TextMeshProUGUI actionText = actionTextTransform.GetComponent<TextMeshProUGUI>();
                actionText.text = currentInteractable.InteractionPrompt;
            }
        }
        else
        {
            // Cleanup - Ensure there is no current Interactable_Prompt 
            currentInteractable = null;

            // Delete the current Interaction_Prompt game object if one exists
            if (currentPromptUI != null)
            {
                Destroy(currentPromptUI);
                currentPromptUI = null;
            }
        }
    }
}
