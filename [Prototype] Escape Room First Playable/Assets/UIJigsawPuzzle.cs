using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJigsawPuzzle : MonoBehaviour
{
    public GameObject[] piecesGO;
    public List<JigsawPiece> pieces;

    // Start is called before the first frame update
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
        CheckSolved();
    }

    public bool CheckSolved()
    {
        // +
        return false;
    }
}

[Serializable]
public struct JigsawPiece
{
    public int index;
    public GameObject piece;
    public int orientation;
}