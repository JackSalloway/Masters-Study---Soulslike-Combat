using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health; // Variable assigned to the enemies health resource. Assigned in the inspection window
    public float poise; // Variable assigned to the enemy's poise resource. Assigned in the inspection window
    public bool poiseBroken = false; // Variable assigned to the enemy's stance status
    public float timeSinceLastHit = 0; // Variable assigned to keep track since the enemy was last hit.
    public float poiseResetTimer = 0; // Variable used to keep the enemy's poise broken until it hits 1.5 seconds
    public bool isDead = false; // Variable assigned to the enemies life status
    
    [Header("Script References")]
    [SerializeField] private EnemyAnimationController enemyAnimController; // Reference to the EnemyAnimationController script

    void Update()
    {
        // Check if the enemy is dead and trigger the death anim + prevent any code being executed if so
        if (isDead == true) {
            enemyAnimController.SetAnimatorState(EnemyAnimationState.Dead); // Set dead animation
            return; // Early return to prevent more code being ran
        }

        // Check if the time since the enemy has been hit is greater than five seconds
        if (timeSinceLastHit < 5) timeSinceLastHit += Time.deltaTime; // Increment the timer until 5 seconds
        else if (poise != 100 && poiseBroken == false) poise += Time.deltaTime; // Slowly increment enemy poise value back to full

        // Whilst the enemy poise is broke, increment a timer to reset the poise back to true
        if (poiseBroken == true)
        {
            poiseResetTimer += Time.deltaTime; // Add 0.02 to the timer per frame
            if (poiseResetTimer >= 3) {
                poiseBroken = false; // Reset the poiseBroken variable to false
                poise = 100; // Reset the value of the enemy's poise to 100
                poiseResetTimer = 0; // Reset the value of the enemy's poise Reset timer to 0
            }
        }       
    }

    // Method to be called when an enemy is hit, dealing damage to their health and poise stats
    public void TakeDamage(float damage, float poiseDamage)
    {
        health -= damage; // Reduce the enemies health by a set amount of damage
        poise -= poiseDamage; // Reduce the enemies poise by a set amount of poise damage
        timeSinceLastHit = 0; // Reset the timer as the enemy was just hit

        // Check if the poise is at 0, trigger stagger animation if it isnt and stance broken if it is
        if (poise <= 0) {
            poiseBroken = true; // Set poiseBroken variable to true
            enemyAnimController.SetAnimatorState(EnemyAnimationState.Stunned); // Set stunned animation
        } else enemyAnimController.SetAnimatorState(EnemyAnimationState.Staggered); // Set staggered animation
        // Check if the enemy is dead or not
        if (health <= 0) isDead = true;
    }

    // Method to be called at the end of the stagger animation to reset the animator controller isStaggered boolean to false
    // void ResetStaggerAnim() {
    //     animator.SetBool("isStaggered", false);
    // } 

    // Method to be called at the end of the stance broken animation to reset the animator controller stanceBroken boolean to false;
    // void ResetStanceBrokeAnim()
    // {
    //     animator.SetBool("stanceBroken", false);
    // }
}
