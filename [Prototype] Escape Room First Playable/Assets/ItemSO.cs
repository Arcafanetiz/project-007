using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int ID => GetInstanceID();
    public enum ItemType { KEY_ITEM, CLUE_ITEM };

    [field: SerializeField]
    public string ItemName { get; set; }
    [field: SerializeField]
    public Sprite Sprite { get; set; }
    [field: SerializeField]
    [field: TextArea]
    public string Desciption { get; set; }
    //public ItemType itemType;
    //public bool combinable;
    //public ItemSO combineTarget;
    //public ItemSO combineResult;
}

