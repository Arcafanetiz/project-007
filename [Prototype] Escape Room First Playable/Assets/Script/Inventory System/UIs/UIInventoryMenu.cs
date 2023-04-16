using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryMenu : MonoBehaviour
    {
        [Header("Private Serialized Field -Do not touch-")]
        [SerializeField] private UIInventoryItem itemSlotPrefab;
        [SerializeField] private UIInventoryInspector inventoryInspector;
        //[SerializeField] private UIMouseFollower mouseFollower;
        [SerializeField] private RectTransform inventoryPanel;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private Button inventoryButton;
        [SerializeField] private Button useItemButton;
        private Sprite defaultBagIcon;
        readonly List<UIInventoryItem> listsOfUIItems = new List<UIInventoryItem>();
        public int currentSelectIndex = -1;
        //private int currentDragIndex = -1;

        public event Action<int> OnDescriptionRequest;
        [HideInInspector] public UnityEvent OnUseItemRequest = new UnityEvent();

        //public event Action<int> OnDescriptionRequest, OnItemActionRequested, OnStartDragging;
        //public event Action<int, int> OnCombineRequest;

        private float originPosY;
        [Header("Animation")]
        public float distance = 150.0f;
        public float angle = 20.0f;
        public float duration = 1.0f;
        public LeanTweenType easeType;

        private void Awake()
        {
            // Reset UI on awake
            originPosY = inventoryPanel.anchoredPosition.y;
            inventoryInspector.ResetInspector();
            ResetUI(0);
            //mouseFollower.Toggle(false);
            useItemButton.onClick.AddListener(OnUseItemRequest.Invoke);
            defaultBagIcon = inventoryButton.GetComponent<Image>().sprite;
        }

        public void InitializeInventoryUI(int inventory_size)
        {
            // Create UI item slots
            for (int i = 0; i < inventory_size; i++)
            {
                UIInventoryItem ui_item = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.Euler(0.0f, 0.0f, -angle));
                ui_item.transform.SetParent(contentPanel);
                ui_item.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                listsOfUIItems.Add(ui_item);
                // Add event to item slot
                ui_item.OnItemClicked += HandleItemSelection;
                //ui_item.OnRightClicked += HandleShowItemAction;
                //ui_item.OnItemBeginDrag += HandleItemBeginDrag;
                //ui_item.OnItemDroppedOn += HandleItemCombine;
                //ui_item.OnItemEndDrag += HandleItemEndDrag;
            }
        }

        public void UpdateData(int item_index, Sprite item_sprite)
        {
            if (listsOfUIItems.Count > item_index)
            {
                listsOfUIItems[item_index].SetData(item_sprite);
            }
        }

        public void ResetAllData()
        {
            foreach (var item_ui in listsOfUIItems)
            {
                item_ui.ResetData();
                item_ui.Deselect();
            }
            ResetBagIcon();
        }

        public void UpdateInspector(int item_index, Sprite item_sprite, string item_name, string item_desc)
        {
            inventoryInspector.SetInspector(item_sprite, item_name, item_desc);
            DeselectAllItems();
            listsOfUIItems[item_index].Select();
            currentSelectIndex = item_index;
        }

        public void ResetSelection()
        {
            Debug.Log("reset");
            inventoryInspector.ResetInspector();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item_ui in listsOfUIItems)
            {
                item_ui.Deselect();
            }
            currentSelectIndex = -1;
        }

        private void HandleItemSelection(UIInventoryItem item_ui)
        {
            int index = listsOfUIItems.IndexOf(item_ui);
            Debug.Log(item_ui.name + "Clicked.");
            if (index == -1)
                return;
            OnDescriptionRequest?.Invoke(index);
        }

        //private void HandleShowItemAction(UIInventoryItem item_ui)
        //{
        //    Debug.Log(item_ui.name + "Right clicked.");
        //}

        //private void HandleItemBeginDrag(UIInventoryItem item_ui)
        //{
        //    int index = listsOfUIItems.IndexOf(item_ui);
        //    if (index == -1)
        //        return;
        //    currentDragIndex = index;
        //    HandleItemSelection(item_ui);
        //    OnStartDragging?.Invoke(index);
        //}

        //private void HandleItemCombine(UIInventoryItem item_ui)
        //{
        //    int index = listsOfUIItems.IndexOf(item_ui);
        //    if (index == -1)    
        //        return;

        //    OnCombineRequest?.Invoke(currentDragIndex, index);
        //}

        //private void HandleItemEndDrag(UIInventoryItem item_ui)
        //{
        //    ResetDraggedItem();
        //}

        public void Toggle()
        {
            // Toggle panel on and off
            if (gameObject.activeInHierarchy == false)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            // Show panel
            gameObject.SetActive(true);
            inventoryInspector.ResetInspector();
            LeanTween.move(inventoryPanel, new Vector3(0.0f, originPosY, 0.0f), duration).setEase(easeType);
            LeanTween.rotate(inventoryPanel, angle, duration).setEase(easeType);
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(easeType).setOnComplete(() => { ResetUI(1); });

            ResetSelection();
        }

        public void Hide()
        {
            // Hide panel
            LeanTween.move(inventoryPanel, new Vector3(0.0f, -originPosY - distance, 0.0f), duration).setEase(easeType);
            LeanTween.rotate(inventoryPanel, -angle, duration).setEase(easeType);
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(easeType).setOnComplete(() => { ResetUI(0); });

            //ResetDraggedItem();
        }

        private void ResetUI(int state)
        {
            // Reset UI
            switch (state)
            {
                case 1:
                    inventoryPanel.anchoredPosition = new Vector3(0.0f, originPosY, 0.0f);
                    inventoryPanel.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    gameObject.GetComponent<CanvasGroup>().alpha = 1;
                    break;
                case 0:
                    inventoryPanel.anchoredPosition = new Vector3(0.0f, originPosY - distance, 0.0f);
                    inventoryPanel.rotation = Quaternion.Euler(0.0f, 0.0f, -angle);
                    gameObject.GetComponent<CanvasGroup>().alpha = 0;
                    gameObject.SetActive(false);
                    break;
            }
        }

        //private void CreateDraggedItem(Sprite item_sprite)
        //{
        //    mouseFollower.Toggle(true);
        //    mouseFollower.SetData(item_sprite);
        //}

        //private void ResetDraggedItem()
        //{
        //    mouseFollower.Toggle(false);
        //    currentDragIndex = -1;
        //}

        public void SetBagIcon(Sprite sprite)
        {
            inventoryButton.GetComponent<Image>().sprite = sprite;
        }

        public void ResetBagIcon()
        {
            inventoryButton.GetComponent<Image>().sprite = defaultBagIcon;
        }

        //public void RemoveUIItem(int index)
        //{
        //    listOfUIItems[index].ResetData();
        //}
    }
}