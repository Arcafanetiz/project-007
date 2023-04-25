using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public bool empty = true;

        [Header("Private Serialized Field -Do not touch-")]
        [SerializeField] private Image itemIconImage;
        [SerializeField] private Image highlightImage;

        public event Action<UIInventoryItem> OnItemClicked;

        public void Awake()
        {
            // Reset item slot and deselect on awake
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            // Reset item slot data
            itemIconImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            // Disable highlight image
            highlightImage.enabled = false;
        }

        public void SetData(Sprite item_icon_sprite)
        {
            // Set item slot data
            itemIconImage.gameObject.SetActive(true);
            itemIconImage.sprite = item_icon_sprite;
            empty = false;
        }

        public void Select()
        {
            // Enable highlight image
            highlightImage.enabled = true;
            AudioManager.instance.PlayAudio("UI Click");
        }

        public void OnPointerClick(PointerEventData pointer_data)
        {
            // Call if the item slot was clicked
            if (pointer_data.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
            }
        }
    }
}