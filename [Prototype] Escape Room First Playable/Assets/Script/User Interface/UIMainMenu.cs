using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;

    [Header("Animation")]
    public float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Main Menu
        mainMenuPanel.SetActive(true);
        mainMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        LeanTween.alphaCanvas(mainMenuPanel.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(LeanTweenType.easeOutExpo);
        // Settings Menu
        settingsMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        settingsMenuPanel.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        settingsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OpenSettingsMenu()
    {
        // Stop Tween
        LeanTween.cancel(mainMenuPanel);
        LeanTween.cancel(settingsMenuPanel);
        // Main Menu
        SetPanelActive(mainMenuPanel);
        LeanTween.alphaCanvas(mainMenuPanel.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(mainMenuPanel.GetComponent<RectTransform>(), new Vector3(0.5f, 0.5f, 0.5f), duration).setEase(LeanTweenType.easeOutExpo).setOnComplete(() => SetPanelInactive(mainMenuPanel, 0));
        // Settings Menu
        SetPanelInactive(settingsMenuPanel, 1);
        settingsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        LeanTween.alphaCanvas(settingsMenuPanel.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(settingsMenuPanel.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f, 1.0f), duration).setEase(LeanTweenType.easeOutExpo).setOnComplete(() => SetPanelActive(settingsMenuPanel));
    }

    public void ReturnToMainMenu()
    {
        // Stop Tween
        LeanTween.cancel(mainMenuPanel);
        LeanTween.cancel(settingsMenuPanel);
        // Main Menu
        SetPanelInactive(mainMenuPanel, 0);
        mainMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        LeanTween.alphaCanvas(mainMenuPanel.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(mainMenuPanel.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f, 1.0f), duration).setEase(LeanTweenType.easeOutExpo).setOnComplete(() => SetPanelActive(mainMenuPanel));
        // Settings Menu
        SetPanelActive(settingsMenuPanel);
        LeanTween.alphaCanvas(settingsMenuPanel.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(settingsMenuPanel.GetComponent<RectTransform>(), new Vector3(1.5f, 1.5f, 1.5f), duration).setEase(LeanTweenType.easeOutExpo).setOnComplete(() => SetPanelInactive(settingsMenuPanel, 1)); 
    }

    private void SetPanelActive(GameObject panelGO)
    {
        panelGO.GetComponent<CanvasGroup>().blocksRaycasts = true;
        panelGO.GetComponent<CanvasGroup>().alpha = 1;
        settingsMenuPanel.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void SetPanelInactive(GameObject panelGO, int dir)
    {
        panelGO.GetComponent<CanvasGroup>().alpha = 0;
        if(dir == 0)
        {
            panelGO.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            panelGO.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        panelGO.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
