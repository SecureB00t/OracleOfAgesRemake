using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemInstance> items = new();

    public void AddItem(ItemInstance itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(ItemInstance itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    public ItemInstance GetItem(ItemData itemData)
    {
        return items.Find(item => item.itemType == itemData);
    }

    public void AddAmmo(ItemData itemData, int amount)
    {
        ItemInstance itemInstance = GetItem(itemData);
        if (itemInstance != null)
        {
            itemInstance.ammo += amount;
        }
    }

    public void RemoveAmmo(ItemData itemData, int amount)
    {
        ItemInstance itemInstance = GetItem(itemData);
        if (itemInstance != null && itemInstance.ammo >= amount)
        {
            itemInstance.ammo -= amount;

        }
    }


    public int GetAmmo(ItemData itemData)
    {
        ItemInstance itemInstance = GetItem(itemData);

        if (itemInstance != null)
        {
            return itemInstance.ammo;
        }

        return 0;
    }
}


