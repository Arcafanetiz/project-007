using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(ItemSO item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].isEmpty)
            {
                //+Add Item
                inventoryItems[i] = new InventoryItem
                {
                    item = item
                };
            }
        }
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].isEmpty) { continue; }
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }
}

[Serializable]
public struct InventoryItem
{
    public ItemSO item;
    public bool isEmpty => item == null;

    public static InventoryItem GetEmptyItem() => new InventoryItem
    {
        item = null
    };
}