using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDetection : MonoBehaviour
{
    public List<GameObject> detectedEnemies = new List<GameObject>(); // List to store the enemies within radius of the player

    [SerializeField] private float detectionRadius; // Radius of the physics sphere around the player
    [SerializeField] private List<GameObject> currentlyDetectedEnemies = new List<GameObject>(); // Store a list of currently detected enemies, used for checking if enemies are still within the radius

    // Update is called once per frame
    void Update()
    {
        DetectEnemies();
        CheckEnemies();
    }

    // Method to check if enemies are still present
    public void CheckEnemies()
    {
        // Remove enemies that are no longer present in the current list from the detected list
        // Loop over each enemy in detected enemies in reverse order (reverse order to prevent issues when removing)
        for (int i = detectedEnemies.Count - 1; i >= 0; i--)
        {   
            // Declare a new variable, enemy, set to the detectedEnemies index at the value of i (current for loop value)
            GameObject enemy = detectedEnemies[i];

            // If enemy is no longer present in current list, reomve it from the detected list
            if (!currentlyDetectedEnemies.Contains(enemy)) detectedEnemies.RemoveAt(i);
        }

        // Add newly detected enemies
        foreach (var enemy in currentlyDetectedEnemies)
        {
            // Add the enemy to the detected list if they are not already in it
            if (!detectedEnemies.Contains(enemy)) detectedEnemies.Add(enemy);
        }
    }
}