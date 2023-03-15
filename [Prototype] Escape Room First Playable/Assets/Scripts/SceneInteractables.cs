using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInteractables : MonoBehaviour
{
    private Vector3 originalPos;

    [Header("Events")]
    public UnityEvent OnClickEvent;
    public UnityEvent OnItemUseEvent;

    [Header("Item")]
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private ItemSO requiredItem;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 1.0f;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (OnClickEvent == null)
            OnClickEvent = new UnityEvent();
        if (OnItemUseEvent == null)
            OnItemUseEvent = new UnityEvent();
        originalPos = this.transform.position;
    }

    public void ItemRequestHandler(ItemSO item)
    {
        if(item == requiredItem)
            OnItemUseEvent?.Invoke();
        else
            OnClickEvent?.Invoke();
    }

    public void ItemPickUp(ItemSO item)
    {
        PlayAudio();
        inventoryData.AddItem(item);
        GetComponent<Collider2D>().enabled = false;
        Vector3 endScale = Vector3.zero;
        LeanTween.scale(this.gameObject, endScale, animationDuration).setEase(LeanTweenType.easeInElastic).setOnComplete(() => { this.gameObject.SetActive(false); }); ;
    }

    public void Shake()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.move(this.gameObject, this.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0.0f), animationDuration).setEase(LeanTweenType.easeShake).setOnComplete(() => { this.transform.position = originalPos; });
    }

    public void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
