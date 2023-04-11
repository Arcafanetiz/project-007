using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler //, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    [HideInInspector] public bool empty = true;

    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Image itemIconImage;
    [SerializeField] private Image highlightImage;

    public event Action<UIInventoryItem> OnItemClicked;
    //public event Action<UIInventoryItem> OnItemClicked, OnRightClicked, OnItemBeginDrag, OnItemDroppedOn, OnItemEndDrag;

    public void Awake()
    {
        // Reset item slot and deselect on awake
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        // Reset item slot data
        this.itemIconImage.gameObject.SetActive(false);
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
        this.itemIconImage.gameObject.SetActive(true);
        this.itemIconImage.sprite = item_icon_sprite;
        empty = false;
    }

    public void Select()
    {
        // Enable highlight image
        highlightImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData pointer_data)
    {
        // Call if the item slot was clicked
        if(pointer_data.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
        //else
        //{
        //    OnRightClicked?.Invoke(this);
        //}
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    // Call begin drag event if item slot is not empty
    //    if (empty)
    //        return;
    //    OnItemBeginDrag?.Invoke(this);
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
        
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    // Call event if another item was dropped on the slot, deselect dragged item and select the new slot
    //    OnItemDroppedOn?.Invoke(this);
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    // Call reset event if item was dropped outside of sny slot 
    //    OnItemEndDrag?.Invoke(this);
    //}
}
