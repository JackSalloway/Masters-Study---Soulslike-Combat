using UnityEngine;
using UnityEngine.UI;

public class PlayerStamBar : MonoBehaviour
{
    [SerializeField] private Slider slider; // Assigned to the player health slider game object

    [Header("Scipt References")]
    [SerializeField] private PlayerStamina playerStamina; // Reference to the player health script

    // Set max and current value when the script starts
    void Awake()
    {
        slider.maxValue = playerStamina.stamina;
        slider.value = playerStamina.stamina;
    }

    // Update the slider to match the players stamina every frame
    void Update()
    {
        slider.value = playerStamina.stamina;
    }
}
