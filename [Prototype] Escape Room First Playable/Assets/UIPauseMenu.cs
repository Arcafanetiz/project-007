using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private GameObject pauseMenuPanel;

    [Header("Pause Menu Settings")]
    public KeyCode pauseKey = KeyCode.Escape;

    [Header("Animation")]
    public float duration = 0.5f;
    public LeanTweenType easeType = LeanTweenType.easeOutExpo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            if(pauseMenuPanel.activeInHierarchy == false)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }

    public void Show()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void Hide()
    {

    }

    public void changeBGM()
    {
        AudioManager.instance.SwitchBGM("Title BGM");
    }
}
