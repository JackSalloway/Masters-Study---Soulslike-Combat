using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyDetection : MonoBehaviour
{
    public List<GameObject> detectedEnemies = new List<GameObject>(); // List to store the enemies within radius of the player

    [SerializeField] private float detectionRadius; // Radius of the physics sphere around the player

    // Update is called once per frame
    void Update()
    {
        DetectEnemies();
    }

    // Method to detect enemies within the detectionRadius variable
    public void DetectEnemies()
    {
        // Reset the value of the currentlyDetectedEnemies list each time the method is called
        detectedEnemies.Clear();

        // Create an array of colliders within the detection radius range around the player
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Loop over each colliders found 
        foreach(var collider in colliders)
        {   
            // Check for the "Enemy" tag on the collider
            if (collider.CompareTag("Enemy"))
            {   
                // Add the enemy game object to the current list
                detectedEnemies.Add(collider.gameObject);          
            };
        }
    }
}