using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMovement : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 originPos;
    [SerializeField] private Vector3 anchorPos;
    [SerializeField] private bool isZoomIn = false;

    [Header("Camera Sway")]
    public Vector2 swayMultiplier;
    public Vector2 swayDistanceClamp = new Vector2(0.4f, 0.2f);

    [Header("Camera Zoom")]
    public KeyCode zoomKey = KeyCode.LeftShift;
    public float zoomMultiplier = 5.0f;
    public float zoomDuration = 0.2f;
    public Vector2 zoomDistanceClamp = new Vector2(5.0f, 2.5f);

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        mainCam = GetComponentInChildren<Camera>();
        anchorPos = mainCam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToViewportPoint(Input.mousePosition) + new Vector3(-0.5f, -0.5f, 0f);

        if (Input.GetKeyDown(zoomKey) && !isZoomIn)
        {
            float posX = mousePos.x * zoomMultiplier;
            float posY = mousePos.y * zoomMultiplier;
            CameraZoom(new Vector3(Mathf.Clamp(posX, -zoomDistanceClamp.x, zoomDistanceClamp.x), Mathf.Clamp(posY, -zoomDistanceClamp.y, zoomDistanceClamp.y), originPos.z / zoomMultiplier));
        }
        else if (Input.GetKeyUp(zoomKey) && isZoomIn)
        {
            CameraZoom(originPos);
        }

        CameraSway();
    }

    private void CameraSway()
    {
        float posX = anchorPos.x + (mousePos.x * swayMultiplier.x);
        float posY = anchorPos.y + (mousePos.y * swayMultiplier.y);
        mainCam.transform.localPosition = new Vector3(Mathf.Clamp(posX, -swayDistanceClamp.x, swayDistanceClamp.x), Mathf.Clamp(posY, -swayDistanceClamp.y, swayDistanceClamp.y), anchorPos.z);
    }

    private void CameraZoom(Vector3 target_pos)
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, target_pos, zoomDuration);
        isZoomIn = !isZoomIn;
    }
}