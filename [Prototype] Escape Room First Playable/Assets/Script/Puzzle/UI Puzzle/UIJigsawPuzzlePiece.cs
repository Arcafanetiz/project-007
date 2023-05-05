using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJigsawPuzzlePiece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOrientation()
    {
        transform.Rotate(new Vector3(0, 0, -90));
    }

    public int CheckOrientation()
    {
        float zRotation = transform.eulerAngles.z;
        zRotation = zRotation % 360f; 
        int orient = Mathf.RoundToInt(zRotation / 90f) % 4; 
        return orient;
    }
}
