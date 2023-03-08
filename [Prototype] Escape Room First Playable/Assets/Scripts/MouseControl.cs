using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private SceneInteractables interactable;
    [SerializeField] private InventorySO inventoryData;
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
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, interactableLM);

            if (hit.collider != null && !isOverUI)
            {
                Debug.Log("Target: " + hit.collider.gameObject.name);
                interactable = hit.collider.GetComponent<SceneInteractables>();

                ItemSO Helditem = null;
                if (inventoryData.onHandItemIndex != -1)
                {
                    Helditem = inventoryData.GetItemAt(inventoryData.onHandItemIndex).item;
                }
                
                if (interactable != null && Helditem != null)
                {
                    Debug.Log("Item Inteaction: " + hit.collider.gameObject.name);
                    interactable.ItemRequestHandler(Helditem);
                }
                else if (interactable != null)
                {
                    Debug.Log("Non Item Inteaction: " + hit.collider.gameObject.name);
                    interactable.OnClickEvent?.Invoke();
                }
            }
        }
    }
}
