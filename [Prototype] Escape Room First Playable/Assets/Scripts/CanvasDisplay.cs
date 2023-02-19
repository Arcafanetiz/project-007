using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDisplay : MonoBehaviour
{
    public GameObject outputCanvasGO;
    public Image outputImage;
    public VisualClue[] visualClues;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplaySprite(string name)
    {
        outputCanvasGO.SetActive(true);
        VisualClue targetObject = visualClues.FirstOrDefault(o => o.name == name);
        if (targetObject != null)
        {
            // Update the image on the canvas
            outputImage.sprite = targetObject.sprite;
        }
    }
}
