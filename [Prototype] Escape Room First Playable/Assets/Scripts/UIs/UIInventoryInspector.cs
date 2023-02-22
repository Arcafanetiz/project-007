using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryInspector : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMPro.TextMeshProUGUI itemName;
    [SerializeField] private TMPro.TextMeshProUGUI itemDesc;

    public void Awake()
    {
        ResetInspector();
    }

    public void ResetInspector()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemName.text = "";
        this.itemDesc.text = "";
    }

    public void SetInspector(Sprite sprite, string name, string desc)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemName.text = name;
        this.itemDesc.text = desc;
    }
}
