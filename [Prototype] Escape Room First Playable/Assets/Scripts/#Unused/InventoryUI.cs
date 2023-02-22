using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private float screenWidth;
    private float panelWidth;
    private bool isOpen = false;

    public RectTransform inventoryPanel;
    public KeyCode inventoryKey = KeyCode.Space;
    public float tweenDuration = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        panelWidth = inventoryPanel.rect.width;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey) && !isOpen)
        {
            ToggleInventoryPanel();
        }
        else if (Input.GetKeyUp(inventoryKey) && isOpen)
        {
            ToggleInventoryPanel();
        }
    }

    public void ToggleInventoryPanel()
    {
        if (!isOpen)
        {
            PanelTweenAnimation(screenWidth - panelWidth);
        }
        else
        {
            PanelTweenAnimation(screenWidth);
        }
    }

    private void PanelTweenAnimation(float target_pos)
    {
        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, new Vector3(target_pos, 0, 0), tweenDuration).setEase(LeanTweenType.easeOutExpo);
        isOpen = !isOpen;
    }
}
