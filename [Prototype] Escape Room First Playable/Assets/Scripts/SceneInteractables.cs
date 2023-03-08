using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInteractables : MonoBehaviour
{
    public UnityEvent OnClickEvent;
    public UnityEvent OnItemUseEvent;

    [SerializeField] private ItemSO requiredItem;

    [SerializeField] private InventorySO inventoryData;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float duration = 1.0f;

    private void Awake()
    {
        if (OnClickEvent == null)
            OnClickEvent = new UnityEvent();
        if (OnItemUseEvent == null)
            OnItemUseEvent = new UnityEvent();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ItemRequestHandler(ItemSO item)
    {
        if(item == requiredItem)
        {
            Debug.Log("Correct Item");
            OnItemUseEvent?.Invoke();
        }
        else
        {
            Debug.Log("Incorrect Item");
            OnClickEvent?.Invoke();
        }
    }

    public void ItemPickUp(ItemSO item)
    {
        Debug.Log(item.ItemName + " picked up.");
        inventoryData.AddItem(item);
        DestroyItem();
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(PickUpAnimation());
    }

    private IEnumerator PickUpAnimation()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Vector3 endScale = Vector3.zero;
        LeanTween.scale(this.gameObject, endScale, duration).setEase(LeanTweenType.easeInOutElastic);
        yield return null;
    }

    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
