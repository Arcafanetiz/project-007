using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouseFollower : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }

    public void SetData(Sprite item_sprite)
    {
        item.SetData(item_sprite);
    }
}
