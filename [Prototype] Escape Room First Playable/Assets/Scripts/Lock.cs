using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lock : MonoBehaviour
{
    // Start is called before the first frame update

    public bool Interactable = true;
    public GameObject LockCanvas;
    public TMP_Text[] Text;

    public string Password;
    public string[] LockCharacterChoices;
    public int[] _lockCharacterNumber;
    private string _insertedPassord;
    public enum lockState {locked,  unlocked};
    public lockState currentState = lockState.locked;
    void Start()
    {
        _lockCharacterNumber = new int[Password.Length];
        UpdateUI();
    }
    public void ChangeInsertedPasswordIncrease(int number)
    {

        _lockCharacterNumber[number]++;

        if (_lockCharacterNumber[number] >= LockCharacterChoices[number].Length)
        {
            _lockCharacterNumber[number] = 0;
        }

        

        CheckPassword();
        UpdateUI();
    }

    public void ChangeInsertedPasswordDecrease(int number)
    {
        Debug.Log("click");
        
        if (_lockCharacterNumber[number] == 0)
        {
            _lockCharacterNumber[number] = LockCharacterChoices[number].Length;
        }

        _lockCharacterNumber[number]--;

        CheckPassword();
        UpdateUI();
    }

    public void CheckPassword()
    {
        int pass_len = Password.Length;
        _insertedPassord = "";
        for(int i = 0; i < pass_len; i++)
        {
            _insertedPassord += LockCharacterChoices[i][_lockCharacterNumber[i]].ToString();
        }
        if(Password == _insertedPassord)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        Debug.Log("unlocked");
        currentState = lockState.unlocked;

    }

    public void UpdateUI()
    {
        int len = Text.Length;
        for(int i = 0; i < len; i++)
        {
            Text[i].text = LockCharacterChoices[i][_lockCharacterNumber[i]].ToString();
        }
    }

    private void OnMouseDown()
    {
        Interact();
    }
    public void Interact()
    {
        if(Interactable)
        LockCanvas.SetActive(true);
    }

    public void StopInteract()
    {
        LockCanvas.SetActive(false);
    }

}
