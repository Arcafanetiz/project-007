using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private GameObject pauseMenuPanel;
    private bool isPaused;

    [Header("Pause Menu Settings")]
    public KeyCode pauseKey = KeyCode.Escape;

    [Header("Animation")]
    public float duration = 0.5f;
    public LeanTweenType easeType = LeanTweenType.easeOutExpo;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        SetPanelInactive(pauseMenuPanel);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pauseKey) && !isPaused)
        {
            Show();
        }
    }

    public void Show()
    {
        isPaused = true;
        LeanTween.cancel(pauseMenuPanel);
        SetPanelInactive(pauseMenuPanel);
        LeanTween.alphaCanvas(pauseMenuPanel.GetComponent<CanvasGroup>(), 1.0f, duration).setEase(easeType);
        LeanTween.scale(pauseMenuPanel.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f, 1.0f), duration).setEase(easeType).setOnComplete(() => SetPanelActive(pauseMenuPanel));
    }

    public void Hide()
    {
        LeanTween.cancel(pauseMenuPanel);
        SetPanelActive(pauseMenuPanel);
        LeanTween.alphaCanvas(pauseMenuPanel.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(easeType);
        LeanTween.scale(pauseMenuPanel.GetComponent<RectTransform>(), new Vector3(1.5f, 1.5f, 1.5f), duration).setEase(easeType).setOnComplete(() => { SetPanelInactive(pauseMenuPanel); isPaused = false; });
    }

    private void SetPanelActive(GameObject panelGO)
    {
        panelGO.GetComponent<CanvasGroup>().alpha = 1;
        panelGO.GetComponent<CanvasGroup>().blocksRaycasts = true;
        panelGO.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void SetPanelInactive(GameObject panelGO)
    {
        panelGO.GetComponent<CanvasGroup>().alpha = 0;
        panelGO.GetComponent<CanvasGroup>().blocksRaycasts = false;
        panelGO.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void changeBGM()
    {
        AudioManager.instance.SwitchBGM("Title BGM");
    }
}
