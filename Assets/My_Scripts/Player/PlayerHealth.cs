using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Public Variables
    public float health; // Basic player resource used for tracking if the player should be alive or not
    public bool playerIsDead = false; // Boolean used for tracking player death status
    public float flaskHealing = 30; // Variable for storing the amount the player can heal by
    public Animator animator; // Variable assigned to the player animation controller. Assigned in the inspection window

    
    // Update is called once per frame
    void Update()
    {
        // Check if the player is dead and trigger the death animation if so
        if (playerIsDead == true) {
            animator.SetBool("isDead", true); // Trigger player death animation
            return; // Early return to prevent any more code being exectued
        }

        // Simulate the player using a healing item to restore health
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key pressed, healing player by 30.");
            HealPlayer(flaskHealing);
        }
    }

    // Method to damage the player for a specific amount
    public void DamagePlayer(float damage)
    {
        if (playerIsDead) return; // Early return if player is already dead

        health -= damage; // Subtract the damage amount from the player's health pool
        if (health <= 0) playerIsDead = true; // Set the player death status to true if the players health falls below 0
    }

    // Method to heal the player for a specific amount
    public void HealPlayer(float healing)
    {
        health += healing; // Add the healing amount to the player's health pool
        if (health >= 100) health = 100; // Prevent the player's health from going over the max value;
    }
}
