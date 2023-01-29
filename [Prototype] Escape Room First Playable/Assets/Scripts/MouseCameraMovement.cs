using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMovement : MonoBehaviour
{
    private Vector3 mousePos;

    public Vector3 originPos;

    public float xMultiplier;
    public float yMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = new Vector3(originPos.x + ((mousePos.x - 0.5f) * xMultiplier), originPos.y + ((mousePos.y - 0.5f) * yMultiplier), originPos.z);
    }
}
