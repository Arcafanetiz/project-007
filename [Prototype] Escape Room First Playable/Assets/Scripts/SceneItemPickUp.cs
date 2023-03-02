using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneItemPickUp : MonoBehaviour
{
    public UnityEvent OnClickEvent;

    [SerializeField] private InventorySO inventoryData;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float duration = 1.0f;

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
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        LeanTween.scale(this.gameObject, endScale, duration).setEase(LeanTweenType.easeInOutElastic);
        yield return null;
    }
}
