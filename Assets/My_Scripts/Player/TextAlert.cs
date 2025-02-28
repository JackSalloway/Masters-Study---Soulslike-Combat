using UnityEngine;
using TMPro;

public class TextAlert : MonoBehaviour
{
    public TextMeshPro alertText; // Variable assigned to the TMP object above the player character mesh.
    
    private string actionType; // String variable used to discern if the action was a dodge or a parry

    // Method for setting the text value of the TMP object to "!" when the player performs a successful dodge or parry 
    public void AlertPlayer(string action)
    {
        alertText.SetText("!"); // Set the text above the player characters head
        actionType = action; // Set the actionType variable to the action performed by the player
    }
}
