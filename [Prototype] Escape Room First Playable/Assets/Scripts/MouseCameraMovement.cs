using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 originPos;
    [SerializeField] private bool isZoomIn = false;

    [Header("Camera Sway")]
    public Vector3 anchorPos;
    public Vector2 swayMultiplier;

    [Header("Camera Zoom")]
    public KeyCode zoomKey = KeyCode.LeftShift;
    public float zoomMultiplier = 5.0f;
    public float zoomDuration = 0.2f;
    public Vector2 distanceClamp = new Vector2(5.0f, 2.5f);

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

        if (Input.GetKeyDown(zoomKey) && !isZoomIn)
        {
            CameraZoom(new Vector3(Mathf.Clamp(mousePos.x * zoomMultiplier, -distanceClamp.x, distanceClamp.x), Mathf.Clamp(mousePos.y * zoomMultiplier, -distanceClamp.y, distanceClamp.y), originPos.z / zoomMultiplier));
        }
        else if (Input.GetKeyUp(zoomKey) && isZoomIn)
        {
            CameraZoom(originPos);
        }
    }

    void CameraSway()
    {
        mainCam.transform.position = new Vector3(anchorPos.x + (mousePos.x * swayMultiplier.x), anchorPos.y + (mousePos.y * swayMultiplier.y), anchorPos.z);
    }

    void CameraZoom(Vector3 target_pos)
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, target_pos, zoomDuration);
        isZoomIn = !isZoomIn;
    }
}