using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : ScriptableObject
{
    public new string name;

    public virtual void Activated() { }
}
