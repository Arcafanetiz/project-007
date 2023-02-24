using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private SceneInteractables interactable;
    private SceneItemPickUp item;
    [SerializeField] private bool cursorLock = true;

    public LayerMask interactableLM;

    // Start is called before the first frame update
    void Start()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
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
                item = hit.collider.GetComponent<SceneItemPickUp>();
                if (item != null)
                {
                    item.OnClickEvent.Invoke();
                }
            }
        }
    }
}
