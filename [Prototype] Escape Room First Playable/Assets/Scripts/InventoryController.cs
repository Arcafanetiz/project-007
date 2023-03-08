using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] UIInventoryMenu inventoryUI;
    [SerializeField] InventorySO inventoryData;

    public KeyCode inventoryKey = KeyCode.I;

    public Button inventoryButton;
    public Sprite defaultBagIcon;

    // Start is called before the first frame update
    void Start()
    {
        PrepareUI();
        inventoryData.Initialize();
        inventoryData.onHandItemIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    public void OpenInventory()
    {
        inventoryUI.Show();
        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.Sprite);
        }
    }

    public void ToggleInventory()
    {
        if (inventoryUI.isActiveAndEnabled == false)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    public void CloseInventory()
    {
        inventoryUI.Hide();
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty) 
        {
            inventoryUI.ResetSelection();
            return; 
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.Sprite, item.ItemName, item.Desciption);
    }

    private void HandleItemUseRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        ItemSO item = inventoryItem.item;
        //onHandItem = item;
    }

    public void UseItem()
    {
        inventoryData.onHandItemIndex = inventoryUI.selectedIndex;
        if(inventoryUI.selectedIndex != -1)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(inventoryUI.selectedIndex);
            ItemSO item = inventoryItem.item;
            //onHandItem = item;
            inventoryButton.GetComponent<Image>().sprite = item.Sprite;
            CloseInventory();
        }
        else
        {
            inventoryData.onHandItemIndex = -1;
            inventoryButton.GetComponent<Image>().sprite = defaultBagIcon;
        }
    }
}
