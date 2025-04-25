using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Item Values")]
    [SerializeField] private string itemName; // Variable storing the item's name
    [SerializeField] private int itemID; // Variable storing the item's id

    [Header("Script References")]
    [SerializeField] private PlayerInventory playerInventory; // Reference to the PlayerInventory script

    // Method to pick the item up and add it to the players inventory and delete the item from the ground
    public void Interact()
    {
        Item newItem = new(itemName, itemID); // Create new item
        playerInventory.AddItem(newItem); // Add to the player's inventory
        Destroy(gameObject); // Remove item from the scene
    }
}
