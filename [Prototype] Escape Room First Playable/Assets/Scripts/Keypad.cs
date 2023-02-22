using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public TMPro.TextMeshProUGUI displayText;

    public GameObject incorrectPass;

    public string passcode;

    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;

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
        if(displayText.text == passcode)
        {
            displayText.text = "Correct!!!";
            currentState = LockState.UNLOCKED;
        }
        else
        {
            incorrectPass.SetActive(true);
            displayText.text = "";
            //StartCoroutine(Incorrect());
        }
    }
    // private IEnumerator Incorrect()
    // {
    //     displayText.text = "INCORRECT!!";
    //     yield return new WaitForSeconds(2.0f);
    //     displayText.text = "";
    // }
}
