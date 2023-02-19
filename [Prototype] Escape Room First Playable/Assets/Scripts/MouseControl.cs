using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private CanvasDisplay canvasDisplay;
    private SceneInteractables interactable;
    [SerializeField] private bool cursorLock = true;

    public LayerMask interactableLM;

    // Start is called before the first frame update
    void Start()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        canvasDisplay = GetComponent<CanvasDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && !isOverUI)
            {
                Debug.Log("Target: " + hit.collider.gameObject.name);
                interactable = hit.collider.GetComponent<SceneInteractables>();
                if (interactable != null)
                {
                    interactable.OnClickEvent.Invoke();
                }
                //if (hit.collider.gameObject.tag == "Visual Clue")
                //{
                //    canvasDisplay.DisplaySprite(hit.collider.gameObject.name);
                //}
            }
        }
    }
}
