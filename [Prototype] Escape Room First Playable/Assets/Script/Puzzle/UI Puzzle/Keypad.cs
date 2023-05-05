using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;

    public TMPro.TextMeshProUGUI displayText;
    public GameObject incorrectPass;

    public string passcode;

    [Header("Events")]
    public UnityEvent UnlockEvent;

    // Start is called before the first frame update
    private void Start()
    {
        displayText.text = "";
        incorrectPass.SetActive(false);
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
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
                Unlock();
            }
            else
            {
                incorrectPass.SetActive(true);
                displayText.text = "";
            }
        }
    }

    public void Unlock()
    {
        currentState = LockState.UNLOCKED;
        UnlockEvent?.Invoke();
    }
}
