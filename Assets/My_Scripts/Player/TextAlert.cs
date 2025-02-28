using UnityEngine;
using TMPro;

public class TextAlert : MonoBehaviour
{
    public TextMeshPro alertText; // Variable assigned to the TMP object above the player character mesh.
    public string actionType = ""; // String variable used to discern if the action was a dodge or a parry

    private float textTimer = 0; // Variable used to reset the text value of the TMP object after 3 seconds

    void Update()
    {
        alertText.transform.forward = Camera.main.transform.forward; // Make the text rotate with the camera

        if (alertText.text != "") textTimer += Time.deltaTime; // Start the timer if the text value is not an empty string
        
        if (textTimer >= 3) ResetText(); // When the timer is greater than or equal to 3 seconds, call the ResetText method        
    }

    // Method for setting the text value of the TMP object to "!" when the player performs a successful dodge or parry 
    public void AlertPlayer(string action)
    {
        alertText.SetText("!"); // Set the text above the player characters head
        actionType = action; // Set the actionType variable to the action performed by the player
    }

    // Method for reseting variable values to their original values
    private void ResetText() {
        alertText.SetText(""); // Set the text value to an empty string
        actionType = ""; // Set the action type to null
        textTimer = 0; // Set the timer value back to zero
    }
}
