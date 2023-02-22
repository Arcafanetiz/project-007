using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public TMPro.TextMeshProUGUI displayText;

    public string passcode;

    // Start is called before the first frame update
    private void Start()
    {
        displayText.text = "";
    }
    public void Number(int num)
    {
        displayText.text += num.ToString();
    }

    public void Execute()
    {
        if(displayText.text == passcode)
        {
            displayText.text = "Correct!!!";
        }
        else
        {
            StartCoroutine(Incorrect());
        }
    }

    private IEnumerator Incorrect()
    {
        displayText.text = "INCORRECT!!";
        yield return new WaitForSeconds(2.0f);
        displayText.text = "";
    }
}
