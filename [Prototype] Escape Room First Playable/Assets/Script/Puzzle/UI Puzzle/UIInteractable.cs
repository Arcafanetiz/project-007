using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIInteractable : MonoBehaviour
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

    [Header("Audio")]
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

        OnItemUseEvent.AddListener(PlayOnItemUseAudio);
        OnItemUseEvent.AddListener(RemoveOnHandItem);
        //OnItemUseEvent.AddListener(PlayAnimation);

        if(GetComponent<Button>() != null)
            GetComponent<Button>().onClick.AddListener(InteractionHandler);
    }

    private void InteractionHandler()
    {
        if (inventoryData.onHandItemIndex != -1)
        {
            if (inventoryData.GetItemAt(inventoryData.onHandItemIndex).item == requiredItem)
            {
                OnItemUseEvent?.Invoke();
            }
            else
            {
                OnClickEvent?.Invoke();
            }
        }
        else
        {
            OnClickEvent?.Invoke();
        }
    }

    /// -----------------------------------------
    /// - ITEM HANDLER --------------------------
    /// -----------------------------------------

    public void ItemPickUp(ItemSO item)
    {
        inventoryData.AddItem(item, 1);
        GetComponent<Button>().enabled = false;
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
        //LeanTween.cancel(this.gameObject);
        //LeanTween.move(this.gameObject, this.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0.0f), animationDuration).setEase(LeanTweenType.easeShake).setOnComplete(() => { this.transform.position = originalPos; });
    }

    public void Dissapear()
    {
        //gameObject.SetActive(false);
        Vector3 endScale = Vector3.zero;
        LeanTween.scale(this.gameObject, endScale, animationDuration).setEase(LeanTweenType.easeInBack).setOnComplete(() => { this.gameObject.SetActive(false); });
    }

    /// -----------------------------------------
    /// - AUDIO ---------------------------------
    /// -----------------------------------------

    public void PlayOnClickAudio()
    {
        if (onClickAudio == "")
            return;
        PlayAudio(onClickAudio);
    }

    public void PlayOnItemUseAudio()
    {
        if (onItemUseAudio == "")
            return;
        PlayAudio(onItemUseAudio);
    }

    public void PlayAudio(string audio_name)
    {
        AudioManager.instance.PlayAudio(audio_name);
    }

    public void StopAudio(string audio_name)
    {
        AudioManager.instance.StopAudio(audio_name);
    }
}
