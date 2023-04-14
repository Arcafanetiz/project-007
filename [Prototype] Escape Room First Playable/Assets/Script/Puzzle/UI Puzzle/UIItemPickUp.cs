using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemPickUp : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ItemPickUp(ItemSO item)
    {
        Debug.Log(item.ItemName + " picked up.");
        inventoryData.AddItem(item, 1);
    }
}
