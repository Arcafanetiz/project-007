using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 originPos;
    private bool isZoomIn = false;

    [Header("Camera Sway")]
    public Vector3 anchorPos;
    public Vector2 multiplier;

    [Header("Camera Zoom")]
    public float zoomMultiplier = 5.0f;
    public float zoomDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        mainCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        anchorPos = transform.position;
        mousePos = mainCam.ScreenToViewportPoint(Input.mousePosition) + new Vector3(-0.5f, -0.5f, 0f);
        CameraSway();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isZoomIn)
        {
            CameraZoom(new Vector3(mousePos.x * zoomMultiplier, mousePos.y * zoomMultiplier, originPos.z / zoomMultiplier));
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isZoomIn)
        {
            CameraZoom(originPos);
        }
    }

    void CameraSway()
    {
        mainCam.transform.position = new Vector3(anchorPos.x + (mousePos.x * multiplier.x), anchorPos.y + (mousePos.y * multiplier.y), anchorPos.z);
    }

    void CameraZoom(Vector3 target_pos)
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, target_pos, zoomDuration);
        isZoomIn = !isZoomIn;
    }
}