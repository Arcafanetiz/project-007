using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialougeDisplay : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float timer;

    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private TMPro.TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0 && canvasGroup.alpha >= 0.0)
        {
            timer = 0;
            LeanTween.cancel(gameObject);
            LeanTween.alphaCanvas(canvasGroup, 0.0f, 0.5f).setOnComplete(() => { canvasGroup.alpha = 0.0f; });
        }
    }

    public void DisplayDialogue(DialogueTextSO dialogue_data)
    {
        LeanTween.cancel(gameObject);
        timer = dialogue_data.DisplayTime;
        canvasGroup.alpha = 0.0f;
        dialogueText.text = dialogue_data.DialogueText;
        LeanTween.alphaCanvas(canvasGroup, 1.0f, 0.5f).setOnComplete(() => { canvasGroup.alpha = 1.0f; });
    }
}
