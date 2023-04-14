using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueTextSO : ScriptableObject
{
    [field: SerializeField]
    [field: TextArea]
    public string DialogueText { get; set; }

    [field: SerializeField]
    public float DisplayTime { get; set; } = 3.0f;
}
