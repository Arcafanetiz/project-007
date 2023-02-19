using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneItems : MonoBehaviour
{
    private RectTransform itemImage;
    // Start is called before the first frame update
    void Start()
    {
        itemImage = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        LeanTween.move(itemImage, new Vector3(820, -475, 0), 3f).setEase(LeanTweenType.easeOutExpo);
    }
}
