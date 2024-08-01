using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public void AddItem(string itemName)
    {
        if (itemName != "LockKey")
        {
            items.Add(itemName);
        }
        
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}