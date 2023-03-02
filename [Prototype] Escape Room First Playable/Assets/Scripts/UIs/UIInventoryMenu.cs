using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryMenu : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIInventoryInspector itemInspector;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    public int selectedIndex = -1;

    public event Action<int> OnDescriptionRequested;

    private void Awake()
    {
        Hide();
        itemInspector.ResetInspector();
    }

    public void InitializeInventoryUI(int size)
    {
        for (int i = 0; i < size; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemSprite)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemSprite);
        }
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        //Debug.Log(obj.name);
        int index = listOfUIItems.IndexOf(obj);
        if (index == -1) 
        {
            selectedIndex = -1;
            return; 
        }
        OnDescriptionRequested?.Invoke(index);
        if(obj.empty)
        {
            selectedIndex = -1;
        }
        else
        {
            selectedIndex = index;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemInspector.ResetInspector();
        ResetSelection();
    }

    public void ResetSelection()
    {
        selectedIndex = -1;
        itemInspector.ResetInspector();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void UpdateDescription(int itemIndex, Sprite sprite, string name, string desc)
    {
        itemInspector.SetInspector(sprite, name, desc);
        DeselectAllItems();
        listOfUIItems[itemIndex].Select();
    }
}