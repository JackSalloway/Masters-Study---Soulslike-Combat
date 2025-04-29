using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [Header("Script References")]
    [SerializeField] private EnemyStats enemyStats; // Reference to the enemy stats script

    // Set slider values when the script starts
    void Start()
    {
        slider.maxValue = enemyStats.health;
        slider.value = enemyStats.health;
    }

    // Update is called once per frame
    void Update()
    {   
        // Delete the slider if enemy health drops below 0 and the slider still exists
        if (enemyStats.isDead && slider !=null)
        {
            Destroy(slider.gameObject);
            return; // Early return to prevent code being ran
        }

        // Always face the main camera (face the player)
        if (Camera.main != null) transform.forward = Camera.main.transform.forward;

        slider.value = enemyStats.health; // Set slider value to enemy health every frame
    }
}
