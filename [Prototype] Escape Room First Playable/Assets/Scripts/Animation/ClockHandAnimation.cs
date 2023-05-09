using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandAnimation : MonoBehaviour
{
    public Vector3 screenPos;
    public Vector3 worldPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane + 1;

        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector3 direction = (Vector2)worldPos - (Vector2)transform.position;
        gameObject.transform.up = direction;
    }
}
