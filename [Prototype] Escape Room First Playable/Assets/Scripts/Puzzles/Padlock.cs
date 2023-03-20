using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Padlock : MonoBehaviour
{
    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;
    

    public TMPro.TextMeshProUGUI[] displayText;

    public string passcode;
    public string[] LockCharacters;
    public int[] _lockCharacterIndex;
    private string _currentPasscode;

    [Header("Events")]
    public UnityEvent UnlockEvent;

    void Awake()
    {
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        _lockCharacterIndex = new int[passcode.Length];
        UpdateUI();
    }
    public void ChangeInsertedPasswordIncrease(int number)
    {

        _lockCharacterIndex[number]++;

        if (_lockCharacterIndex[number] >= LockCharacters[number].Length)
        {
            _lockCharacterIndex[number] = 0;
        }

        CheckPassword();
        UpdateUI();
    }

    public void ChangeInsertedPasswordDecrease(int number)
    {
        if (_lockCharacterIndex[number] == 0)
        {
            _lockCharacterIndex[number] = LockCharacters[number].Length;
        }

        _lockCharacterIndex[number]--;

        CheckPassword();
        UpdateUI();
    }

    public void CheckPassword()
    {
        int length = passcode.Length;
        _currentPasscode = "";

        for(int i = 0; i < length; i++)
        {
            _currentPasscode += LockCharacters[i][_lockCharacterIndex[i]].ToString();
        }
        if(passcode == _currentPasscode)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        currentState = LockState.UNLOCKED;
        UnlockEvent.Invoke();
    }

    public void UpdateUI()
    {
        int length = displayText.Length;
        for(int i = 0; i < length; i++)
        {
            displayText[i].text = LockCharacters[i][_lockCharacterIndex[i]].ToString();
        }
    }
}
