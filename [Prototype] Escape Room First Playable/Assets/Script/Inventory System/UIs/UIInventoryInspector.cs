using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
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
            itemImage.gameObject.SetActive(false);
            itemName.text = "";
            itemDescription.text = "";
        }

        public void SetInspector(Sprite item_sprite, string item_name, string item_desc)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = item_sprite;
            itemName.text = item_name;
            itemDescription.text = item_desc;
        }
    }
}