using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryInspector : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TMPro.TextMeshProUGUI itemName;
    [SerializeField] private TMPro.TextMeshProUGUI itemDescription;

    public void Awake()
    {
        ResetInspector();
    }

    public void ResetInspector()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemName.text = "";
        this.itemDescription.text = "";
    }

    public void SetInspector(Sprite item_sprite, string item_name, string item_desc)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = item_sprite;
        this.itemName.text = item_name;
        this.itemDescription.text = item_desc;
    }
}
