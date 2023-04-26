using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrowPuzzlePiece : MonoBehaviour
{
    public int initialIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial index and position of the game object
        initialIndex = transform.GetSiblingIndex();   
    }
}
