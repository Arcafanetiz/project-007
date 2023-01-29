using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public LayerMask interactableLM;
    [SerializeField] private bool cursorLock = true;

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
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Debug.Log("Target: " + hit.collider.gameObject.name);
            }
        }
    }
}
