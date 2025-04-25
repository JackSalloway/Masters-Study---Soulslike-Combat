using System;

[Serializable] // Makes the Item class visible in the inspector window
public class Item
{
    public string itemName; // Name of the item
    public int itemID; // ID assigned to the item, used to differentiate between items

    // Constructor used to create new items with a name and id value
    public Item(string name, int id)
    {
        itemName = name;
        itemID = id;
    }
}
