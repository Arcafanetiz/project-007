using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ItemType { KEY_ITEM, CLUE_ITEM };

    public string itemName;
    public Sprite sprite;
    public Color spriteColor;
    [TextArea]
    public string desciption;
    public ItemType itemType;
    public bool combinable;
    public Item combineTarget;
    public Item combineResult;
}
