using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIKeypad : MonoBehaviour
{
    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;

    //public TMPro.TextMeshProUGUI displayText;
    public TMPro.TMP_InputField inputField;
    public GameObject incorrectPass;

    public string passcode;

    [Header("Events")]
    public UnityEvent UnlockEvent;

    // Start is called before the first frame update
    private void Start()
    {
        //displayText.text = "";
        inputField.text = "";
        incorrectPass.SetActive(false);
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
    }
    public void Number(int num)
    {
        if(currentState == LockState.LOCKED && inputField.text.Length < inputField.characterLimit)
        {
            incorrectPass.SetActive(false);
            //displayText.text += num.ToString();
            inputField.text += num.ToString();
        }
    }

    public void Execute()
    {
        if (currentState == LockState.LOCKED)
        {
            if (inputField.text == passcode)
            {
                //displayText.text = "Correct!!!";
                inputField.text = "Correct!!!";
                Unlock();
            }
            else
            {
                incorrectPass.SetActive(true);
                //displayText.text = "";
                inputField.text = "";
            }
        }
    }

    public void Unlock()
    {
        currentState = LockState.UNLOCKED;
        UnlockEvent?.Invoke();
    }
}
