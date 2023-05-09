using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleButtonInvoke : MonoBehaviour
{
    public Button[] buttons;

    public void MultipleInvoke()
    {
        foreach (Button button in buttons)
        {
            if (button != null && button.onClick != null)
                button.onClick.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
