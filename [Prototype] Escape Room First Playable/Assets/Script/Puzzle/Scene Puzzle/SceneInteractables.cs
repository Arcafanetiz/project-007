using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInteractables : MonoBehaviour
{
    private Vector3 originalPos;

    public enum AnimationType { NONE, SHAKE, DISSAPEAR };

    [Header("Events")]
    public UnityEvent OnClickEvent;
    public UnityEvent OnItemUseEvent;

    [Header("Dialogue")]
    public DialogueTextSO dialogueData;

    [Header("Item")]
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private ItemSO requiredItem;

    [Header("Animation")]
    public AnimationType animationName = AnimationType.NONE; 
    [SerializeField] private float animationDuration = 0.5f;
    public string onClickAudio;
    public string onItemUseAudio;

    private void Awake()
    {
        originalPos = this.transform.position;

        if (OnClickEvent == null)
            OnClickEvent = new UnityEvent();
        if (OnItemUseEvent == null)
            OnItemUseEvent = new UnityEvent();

        OnClickEvent.AddListener(PlayAnimation);
        OnClickEvent.AddListener(PlayOnClickAudio);
        OnClickEvent.AddListener(DisplayDialogue);

        OnItemUseEvent.AddListener(PlayOnItemUseAudio);
        OnItemUseEvent.AddListener(RemoveOnHandItem);
    }

    /// -----------------------------------------
    /// - ITEM HANDLER --------------------------
    /// -----------------------------------------

    public void ItemPickUp(ItemSO item)
    {
        inventoryData.AddItem(item, 1);
        GetComponent<Collider2D>().enabled = false;
    }

    public void ItemRequestHandler(ItemSO item)
    {
        if(item == requiredItem)
            OnItemUseEvent?.Invoke();
        else
            OnClickEvent?.Invoke();
    }

    public void RemoveOnHandItem()
    {
        inventoryData.RemoveItem(inventoryData.onHandItemIndex);
        inventoryData.onHandItemIndex = -1;
    }

    /// -----------------------------------------
    /// - ANIMATION -----------------------------
    /// -----------------------------------------

    public void PlayAnimation()
    {
        switch (animationName)
        {
            case AnimationType.NONE:
                break;
            case AnimationType.SHAKE:
                Shake();
                break;
            case AnimationType.DISSAPEAR:
                Dissapear();
                break;
        }
    }

    public void Shake()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.move(this.gameObject, this.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0.0f), animationDuration).setEase(LeanTweenType.easeShake).setOnComplete(() => { this.transform.position = originalPos; });
    }

    public void Dissapear()
    {
        Vector3 endScale = Vector3.zero;
        LeanTween.scale(this.gameObject, endScale, animationDuration).setEase(LeanTweenType.easeInElastic).setOnComplete(() => { this.gameObject.SetActive(false); });
    }

    /// -----------------------------------------
    /// - AUDIO ---------------------------------
    /// -----------------------------------------

    public void PlayOnClickAudio()
    {
        AudioManager.instance.PlayAudio(onClickAudio);
    }

    public void PlayOnItemUseAudio()
    {
        AudioManager.instance.PlayAudio(onItemUseAudio);
    }

    public void PlayCustomAudio(string audio_name)
    {
        AudioManager.instance.PlayAudio(audio_name);
    }

    /// -----------------------------------------
    /// - DIALOGUE ------------------------------
    /// -----------------------------------------

    public void DisplayDialogue()
    {
        if (dialogueData == null)
            return;
        DisplayDialogue(dialogueData);
    }

    public void DisplayDialogue(DialogueTextSO dialogue_data)
    {
        FindObjectOfType<UIDialougeDisplay>().DisplayDialogue(dialogue_data);
    }
}
