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
            OpenPanel();
        }
        else if (Input.GetKeyUp(inventoryKey) && isOpen)
        {
            ClosePanel();
        }
    }

    public void ToggleInventoryPanel()
    {
        if (!isOpen)
        {
            LeanTween.cancel(inventoryPanel);
            LeanTween.move(inventoryPanel, new Vector3(screenWidth - panelWidth, 0, 0), tweenDuration).setEase(LeanTweenType.easeOutExpo);
            isOpen = true;
        }
        else
        {
            LeanTween.cancel(inventoryPanel);
            LeanTween.move(inventoryPanel, new Vector3(screenWidth, 0, 0), tweenDuration).setEase(LeanTweenType.easeOutExpo);
            isOpen = false;
        }
    }

    public void OpenPanel()
    {
        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, new Vector3(screenWidth - panelWidth, 0, 0), tweenDuration).setEase(LeanTweenType.easeOutExpo);
        isOpen = true;
    }

    public void ClosePanel()
    {
        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, new Vector3(screenWidth, 0, 0), tweenDuration).setEase(LeanTweenType.easeOutExpo);
        isOpen = false;
    }
}
