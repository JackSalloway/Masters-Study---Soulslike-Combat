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

    // Method to check if a player has an item or not, useful for checking if the player can open locked doors
    public bool HasItem(Item item)
    {
        return items.Contains(item); // Returns true if the item is found, false if it isn't
    }
}
