using UnityEngine;
using TMPro;

public class TextAlert : MonoBehaviour
{
    public TextMeshPro alertText; // Variable assigned to the TMP object above the player character mesh.

    // Method for setting the text value of the TMP object to the provided string
    // String examples: "", "Parried!", "Dodged!" 
    public void SetText(string text)
    {
        alertText.SetText(text);
    }
}
