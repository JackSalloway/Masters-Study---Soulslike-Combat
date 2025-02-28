using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage; // Variable used to store the damage value of the weapon

    [SerializeField] private EnemyStats enemy; // Variable assigned to the EnemyStats script component

    // OnTirggerEnter triggers when the object this code is assigned to collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the other collider has the "Player" tag
        {
            // Assign new player variable to the PlayerHealth script component on the player game object
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            // Assign new playerMovement variable to the PlayerMovement script component on the player game object
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Check if the player is parrying
            if (player.animator.GetBool("isParrying") == true) {
                enemy.poise -= 1000; // Deal a very large amount of poise damage to the enemy
                enemy.poiseBroken = true; // Set poiseBroken variable to true to start the timer for resetting poise
                enemy.animator.SetBool("stanceBroken", true); // Trigger the stance broken animation
                return; // Early return to prevent any more code being ran
            };

            // Check if the players iframes are currently active. If false: deal damage to the player. If true: print a message to the console
            if (playerMovement.invulnerabilityFramesActive == false) player.DamagePlayer(damage); // Make the player take damage!
            else Debug.Log("Attack Dodged!"); // Write a message in console saying the attack was dodged
        }
    }
}