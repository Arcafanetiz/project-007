using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] UIInventoryMenu inventoryUI;

    public KeyCode inventoryKey = KeyCode.I;
    public int inventorySize = 15;

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }

        }
    }
}
