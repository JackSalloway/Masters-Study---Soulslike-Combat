using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>(); // Variable that stores a list of items in the players inventory

    // Method to add a new item to the player's inventory
    public void AddItem(Item item)
    {
        items.Add(item); // Adds a new item to the items list
    }

    // Method to check if a player has an item or not based on ID value, useful for checking if the player can open locked doors
    public bool HasItemByID(int id)
    {
        // Loop over each item in the list
        foreach (Item item in items)
        {
            // Check the item's id against the provided id for each iteration and return true if it matches
            if (item.itemID == id)
            {
                return true;
            }
        }

        return false; // Return false otherwise
    }
}
