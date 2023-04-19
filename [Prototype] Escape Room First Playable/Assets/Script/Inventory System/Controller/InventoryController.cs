using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Private Serialized Field -Do not touch-")]
        [SerializeField] private UIInventoryMenu inventoryMenu;

        [Header("Inventory Data")]
        public InventorySO inventoryData;
        public List<InventoryItem> initialItems = new List<InventoryItem>();

        // Start is called before the first frame update
        void Start()
        {
            // Initialized InventorySO and UI
            PrepareInventoryUI();
            PrepareInventoryData();
        }

        // Update is called once per frame
        void Update()
        {
            if (inventoryMenu.isActiveAndEnabled)
            {
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryMenu.UpdateData(item.Key, item.Value.item.Sprite);
                }
            }
        }

        private void PrepareInventoryUI()
        {
            inventoryMenu.InitializeInventoryUI(inventoryData.Size);
            inventoryMenu.OnDescriptionRequest += HandleDescriptionRequest;
            inventoryMenu.OnUseItemRequest.AddListener(HandleUseItemRequest);
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (var item in initialItems)
            {
                if (item.isEmpty) { continue; }
                inventoryData.AddItem(item);
            }
        }

        private void OnDisable()
        {
            inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventory_state)
        {
            inventoryMenu.ResetAllData();
            foreach (var item in inventory_state)
            {
                inventoryMenu.UpdateData(item.Key, item.Value.item.Sprite);
            }
            inventoryMenu.BagAnimation();
        }

        private void HandleDescriptionRequest(int item_index)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(item_index);
            if (inventoryItem.isEmpty || inventoryItem.quantity == 0)
            {
                inventoryMenu.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryMenu.UpdateInspector(item_index, item.Sprite, item.ItemName, item.Desciption);
        }

        private void HandleUseItemRequest()
        {
            inventoryData.onHandItemIndex = inventoryMenu.currentSelectIndex;
            if (inventoryMenu.currentSelectIndex != -1)
            {
                InventoryItem inventoryItem = inventoryData.GetItemAt(inventoryMenu.currentSelectIndex);
                ItemSO item = inventoryItem.item;
                inventoryMenu.SetBagIcon(item.Sprite);
                inventoryMenu.Hide();
            }
            else
            {
                inventoryData.onHandItemIndex = -1;
                inventoryMenu.ResetBagIcon();
            }
        }

        //public void ResetOnHandItem()
        //{
        //    inventoryData.RemoveItem(inventoryData.onHandItemIndex);
        //    inventoryData.onHandItemIndex = -1;
        //    inventoryMenu.ResetBagIcon();
        //}
    }
}