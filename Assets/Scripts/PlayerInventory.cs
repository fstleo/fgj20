using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    
}

public class PlayerInventory
{
    public List<Item> Items;
    
    public void AddItem(Item item)
    {
        Items.Add(item);
    }
}
