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
    public LeanTweenType easeType = LeanTweenType.easeOutExpo;

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
        SetPanelActive(mainMenuPanel);
        SetPanelInactive(settingsMenuPanel, 1);
        mainMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        settingsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        // Main Menu
        LeanTween.alphaCanvas(mainMenuPanel.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(easeType);
        LeanTween.scale(mainMenuPanel.GetComponent<RectTransform>(), new Vector3(0.5f, 0.5f, 0.5f), duration).setEase(easeType).setOnComplete(() => SetPanelInactive(mainMenuPanel, 0));
        // Settings Menu
        LeanTween.alphaCanvas(settingsMenuPanel.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(easeType);
        LeanTween.scale(settingsMenuPanel.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f, 1.0f), duration).setEase(easeType).setOnComplete(() => SetPanelActive(settingsMenuPanel));
    }

    public void ReturnToMainMenu()
    {
        // Stop Tween
        LeanTween.cancel(mainMenuPanel);
        LeanTween.cancel(settingsMenuPanel);
        SetPanelInactive(mainMenuPanel, 0);
        SetPanelActive(settingsMenuPanel);
        mainMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        settingsMenuPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        // Main Menu
        LeanTween.alphaCanvas(mainMenuPanel.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(easeType);
        LeanTween.scale(mainMenuPanel.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f, 1.0f), duration).setEase(easeType).setOnComplete(() => SetPanelActive(mainMenuPanel));
        // Settings Menu
        LeanTween.alphaCanvas(settingsMenuPanel.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(easeType);
        LeanTween.scale(settingsMenuPanel.GetComponent<RectTransform>(), new Vector3(1.5f, 1.5f, 1.5f), duration).setEase(easeType).setOnComplete(() => SetPanelInactive(settingsMenuPanel, 1)); 
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
