using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBookshelfPuzzle : MonoBehaviour
{
    [Header("Private Serialized Field -Do Not Touch-")]
    [SerializeField] private string _currentPasscode = "";
    [SerializeField] private Button[] books;

    [Header("Passcode")]
    public string passcode;

    [Header("Events")]
    public UnityEvent UnlockEvent;

    void Awake()
    {
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start() { }

    public void Unlock()
    {
        UnlockEvent?.Invoke();
    }

    public void AddCharacter(string character)
    {
        _currentPasscode += character;
        CheckPasscode();
    }

    private void CheckPasscode()
    {
        if (_currentPasscode.Equals(passcode))
        {
            Unlock();
        }
        else if(_currentPasscode.Length == passcode.Length)
        {
            Reset();
        }
    }

    public void Reset()
    {
        foreach(Button book in books)
        {
            book.interactable = true;
        }
        _currentPasscode = "";
    }
}
