using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private InventorySO inventoryData;

    [Header("Interactable")]
    public LayerMask interactableLM;

    [Header("Cursor")]
    [SerializeField] private bool cursorLock = true;
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Vector2 defaultCursorHotspot = new Vector2 (3.0f, 3.0f);
    [SerializeField] private Texture2D activeCursor;
    [SerializeField] private Vector2 activeCursorHotspot = new Vector2(12.0f, 3.0f);

    // Start is called before the first frame update
    void Start()
    {
        if (cursorLock)
            Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLM);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && !isOverUI)
            {
                SceneInteractables interactable = hit.collider.GetComponent<SceneInteractables>();

                ItemSO Helditem;
                if (inventoryData.onHandItemIndex != -1)
                    Helditem = inventoryData.GetItemAt(inventoryData.onHandItemIndex).item;
                else
                    Helditem = null;
                
                if (interactable != null && Helditem != null)
                    interactable.ItemRequestHandler(Helditem);
                else if (interactable != null)
                {
                    interactable.OnClickEvent?.Invoke();
                }  
            }
        }

        if (hit.collider != null && !isOverUI)
            Cursor.SetCursor(activeCursor, activeCursorHotspot, CursorMode.Auto);
        else
            Cursor.SetCursor(defaultCursor, defaultCursorHotspot, CursorMode.Auto);
    }
}
