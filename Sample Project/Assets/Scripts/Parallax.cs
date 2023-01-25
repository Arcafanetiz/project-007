using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 targetPos;

    public float multiplier;

    void Start()
    {
        //startPos = transform.position;
    }

    void Update()
    {
        Vector3 targetPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = new Vector3(startPos.x + (targetPos.x * multiplier), startPos.y + (targetPos.y * multiplier), startPos.z);
    }
}