using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Tutorial Values")]
    [SerializeField] private float tabHoldDuration; // Variable used to store how long the player needs to hold the TAB key to toggle tutorials
    [SerializeField] private float tabHoldTimer; // Variable used to track how long the TAB key is held for

    [Header("Script References")]
    [SerializeField] private TuitionData tutorialController; // Reference to the TuitionData script
    [SerializeField] private PlayerCamera playerCamera; // Reference to the PlayerCamera script

    // Update is called once per frame
    void Update()
    {   
        // Check if the player is holding the TAB key.
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
    }
}
