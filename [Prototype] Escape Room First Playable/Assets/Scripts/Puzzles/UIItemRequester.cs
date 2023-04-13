using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIItemRequester : MonoBehaviour
{
    [SerializeField] private ItemSO requiredItem;

    [SerializeField] private InventorySO inventoryData;

    public UnityEvent OnClickEvent;
    public UnityEvent OnItemUseEvent;

    public string onItemUseAudio;

    // Start is called before the first frame update
    void Start()
    {
        OnItemUseEvent.AddListener(RemoveOnHandItem);
        OnItemUseEvent.AddListener(PlayOnItemUseAudio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemRequestHandler()
    {
        if(inventoryData.onHandItemIndex != -1)
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

    public void RemoveOnHandItem()
    {
        inventoryData.RemoveItem(inventoryData.onHandItemIndex);
        inventoryData.onHandItemIndex = -1;
    }

    public void PlayOnItemUseAudio()
    {
        AudioManager.instance.PlayAudio(onItemUseAudio);
    }
}
