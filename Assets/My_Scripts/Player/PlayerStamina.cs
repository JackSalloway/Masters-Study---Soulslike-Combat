using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    // Public Variables
    public float stamina; // Basic player resource used for tracking if the player can perform an action or not
    public bool actionUsed = false; // Variable used to decide when the timer variable below should start
    public float timeSinceLastAction = 0; // Variable used to count time since the last action
    public bool regenStamina = false; // Variable used to decide if stamina should be regenerating or not
    public float staminaRegenRate; // Flat rate at which stamina starts regenerating

    void Update()
    {
        // Check if the player has recently used an action
        if (actionUsed == true) {
            // Check if the time since the last action is equal to or greater than 1 second
            // If greater than 1 second, start the next stage of the process
            // If less than 1 second, keep counting up to 1 second.
            if (timeSinceLastAction >= 1)
            {
                regenStamina = true; // Set the regenStamina variable to true
                actionUsed = false; // Set the actionUsed variable to false
            }
            else timeSinceLastAction += Time.deltaTime;
        }

        // Check if the players stamina should start regenerating and call relevant method if so
        if (regenStamina == true) RegenStamina();
    }

    // Method to remove a specific amount of stamina
    public void SpendStamina(float stamAmount)
    {   
        if (stamina == 0) return; // Early return if stamina equals 0

        stamina -= stamAmount; // Remove stamAmount value from total stamina pool
        if (stamina < 0) stamina = 0; // Never let stamina value go lower than 0
        actionUsed = true; // Set the actionUsed variable to true to prevent instant stamina regen
        timeSinceLastAction = 0; // Reset the timer variable to 0
        regenStamina = false; // set regenStamina to false to prevent instant stamina regen
    }

    // Method to replenish stamina pool when specific conditions are met
    private void RegenStamina()
    {
        stamina += staminaRegenRate * Time.deltaTime; // Slowly add to stamina pool
        // Prevent stamina pool from filling over max stamina amount and stop stamina regen
        if (stamina >= 100) 
        {   
            stamina = 100; // Set stamina to max amount available
            regenStamina = false; // Stop stamina regen
        } 
    }

}
