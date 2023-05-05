using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIJigsawPuzzle : MonoBehaviour
{
    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;

    public GameObject[] piecesGO;
    public List<JigsawPiece> pieces;

    [Header("Events")]
    public UnityEvent UnlockEvent;

    // Start is called before the first frame update
    void Awake()
    {
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
    }
    void Start()
    {
        for (int i = 0; i < piecesGO.Length; i++)
        {
            int rand_orientation = UnityEngine.Random.Range(0, 4);
            pieces.Add(new JigsawPiece { index = i, piece = piecesGO[i], orientation = rand_orientation });
            pieces[i].piece.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, pieces[i].orientation * 90.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckSolved())
        {
            Unlock();
        }
        
    }

    public void Unlock()
    {
        currentState = LockState.UNLOCKED;
        UnlockEvent.Invoke();
    }

    public bool CheckSolved()
    {
        for (int i = 0; i < piecesGO.Length; i++)
        {
            UIJigsawPuzzlePiece puzzlePieceScript = pieces[i].piece.GetComponent<UIJigsawPuzzlePiece>();
            int currentOrientation = puzzlePieceScript.CheckOrientation();
            if (currentOrientation != 0)
            {
                return false;
            }
        }
        return true;
    }
}



[Serializable]
public struct JigsawPiece
{
    public int index;
    public GameObject piece;
    public int orientation;
}