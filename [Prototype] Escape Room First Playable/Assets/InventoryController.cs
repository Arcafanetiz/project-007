using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private UIInventoryMenu inventoryMenu;

    [Header("Inventory Initialization")]
    public int inventorySize = 10;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.InitializeInventoryUI(inventorySize);
    }
}
