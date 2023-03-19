using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemSO : ItemSO, IDestroyableItem, IItemAction
{
    public AudioClip acttionSFX { get; private set; }
}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public AudioClip acttionSFX { get; }
}
