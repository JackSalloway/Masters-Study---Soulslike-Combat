using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider; // Assigned to the player health slider game object

    [Header("Scipt References")]
    [SerializeField] private PlayerHealth playerHealth; // Reference to the player health script

    // Set max and current value when the script starts
    void Awake()
    {
        slider.maxValue = playerHealth.health;
        slider.value = playerHealth.health;
    }

    // Update the slider to match the players health every frame
    void Update()
    {
        slider.value = playerHealth.health;
    }
}
