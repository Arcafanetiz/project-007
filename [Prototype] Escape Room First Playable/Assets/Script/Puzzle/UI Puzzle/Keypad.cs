using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;

    public TMPro.TextMeshProUGUI displayText;
    public GameObject incorrectPass;

    public string passcode;
    
    // Start is called before the first frame update
    private void Start()
    {
        displayText.text = "";
        incorrectPass.SetActive(false);
    }
    public void Number(int num)
    {
        if(currentState == LockState.LOCKED)
        {
            incorrectPass.SetActive(false);
            displayText.text += num.ToString();
        }
      
    }

    public void Execute()
    {
        if (currentState == LockState.LOCKED)
        {
            if (displayText.text == passcode)
            {
                displayText.text = "Correct!!!";
                currentState = LockState.UNLOCKED;
            }
            else
            {
                incorrectPass.SetActive(true);
                displayText.text = "";
            }
        }
    }
}
