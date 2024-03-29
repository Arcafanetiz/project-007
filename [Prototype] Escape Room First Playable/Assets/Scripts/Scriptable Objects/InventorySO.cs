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

    public int onHandItemIndex;
    public int combineItemAIndex;
    public int combineItemBIndex;

    public bool resetOnStart;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
        if(resetOnStart)
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
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
                return;
            }
        }
    }

    public void RemoveItem(int index)
    {
        //+Remove Item
        inventoryItems[index] = InventoryItem.GetEmptyItem();
        //inventoryItems.Remove(inventoryItems[index]);
        //inventoryItems.Add(InventoryItem.GetEmptyItem());
        InformAboutChange();
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
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

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
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