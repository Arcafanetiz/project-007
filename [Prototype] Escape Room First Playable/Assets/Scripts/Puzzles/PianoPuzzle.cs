using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PianoPuzzle : MonoBehaviour
{
     public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;

    public string passcode;

   public string _currentPasscode = "";

    public float resetTime = 300f;
    private float _timeSinceLastInput = 0f;

    // Start is called before the first frame update

     [Header("Events")]
     public UnityEvent UnlockEvent;

    void Awake()
    {
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
    }
    void Start()
    {
        
    }
    public void Unlock()
    {
        currentState = LockState.UNLOCKED;
        UnlockEvent.Invoke();
    }

    public void AddCharacter(string character)
    {
        _currentPasscode += character;
        _timeSinceLastInput = 0f;
        CheckPasscode();
    }
    private void CheckPasscode()
    {
        if (_currentPasscode.Equals(passcode))
        {
            currentState = LockState.UNLOCKED;
            UnlockEvent.Invoke();
        }
    }

    void Update()
    {
        _timeSinceLastInput += Time.deltaTime;
        if (_timeSinceLastInput >= resetTime)
        {
            _currentPasscode = "";
            _timeSinceLastInput = 0f;
        }
    }
}
