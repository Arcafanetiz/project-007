using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPasswordInputField : MonoBehaviour 
{
    [Header("Private Serialized Field -Do not touch-")]
    public TMPro.TMP_InputField inputField;
    public GameObject incorrectText;
    public GameObject correctText;

    [Header("Password Setting")]
    public string correctPassword;

    [Header("Events")]
    public UnityEvent UnlockEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (UnlockEvent == null)
            UnlockEvent = new UnityEvent();
        incorrectText.SetActive(false);
        correctText.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    public void CheckPassword()
    {
        if(inputField.text == correctPassword)
        {
            Debug.Log("Correct Password Entered.");
            incorrectText.SetActive(false);
            correctText.SetActive(true);
            UnlockEvent?.Invoke();
        }
        else
        {
            incorrectText.SetActive(true);
            correctText.SetActive(false);
        }
    }
}
