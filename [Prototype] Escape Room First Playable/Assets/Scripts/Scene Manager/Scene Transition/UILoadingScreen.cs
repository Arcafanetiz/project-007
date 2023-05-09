using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : MonoBehaviour
{
    [Header("Animation")]
    public float duration = 0.7f;
    public LeanTweenType easeType = LeanTweenType.easeOutExpo;

    public void Hide()
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0.0f, duration).setEase(easeType).setOnComplete(() => { gameObject.SetActive(false); });
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    // Update is called once per frame
    void Update() { }
}
