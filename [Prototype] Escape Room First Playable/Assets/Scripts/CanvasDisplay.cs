using System.Collections;
using System.Collections.Generic;
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
        for (int i = 0; i < visualClues.Length; i++)
        {
            if (visualClues[i].name == name)
            {
                outputImage.sprite = visualClues[i].sprite;
            }
        }
    }
}
