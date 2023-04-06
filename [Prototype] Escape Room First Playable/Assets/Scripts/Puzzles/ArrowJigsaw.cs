using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ArrowJigsaw : MonoBehaviour
{

    public enum LockState { LOCKED, UNLOCKED };
    public LockState currentState = LockState.LOCKED;


    public Button[] buttons;
    public int emptyIndex = 3;


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
      //Restart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check if a button can be moved based on its index
   public void CheckMoveRight(Button button)
    {
        int index = -1;

        // Find the index of the button in the array
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == button)
            {
                index = i;
                break;
            }
        }

        // Check if the button can be moved to the empty space
        if (index + 1 == emptyIndex || index + 2 == emptyIndex)
        {
            // Move the button to the right of the empty space
            SwapButtons(index, emptyIndex);
        }
    }

public void CheckMoveLeft(Button button)
{
    int index = -1;

    // Find the index of the button in the array
    for (int i = 0; i < buttons.Length; i++)
    {
        if (buttons[i] == button)
        {
            index = i;
            break;
        }
    }

    // Check if the button can be moved to the empty space
    if (index - 1 == emptyIndex || index - 2 == emptyIndex)
    {
        // Move the button to the right of the empty space
        SwapButtons(index, emptyIndex);
    }
}


// Swap the positions of two buttons in the array
void SwapButtons(int index1, int index2)
{
        Vector3 tempPos = buttons[index1].transform.position;
        buttons[index1].transform.position = buttons[index2].transform.position;
        buttons[index2].transform.position = tempPos;

            // Swap the buttons' indices in the array
        Button tempButton = buttons[index1];
        buttons[index1] = buttons[index2];
        buttons[index2] = tempButton;

        // Update the empty index to the new index of the empty space
        if (index1 == emptyIndex)
        {
            emptyIndex = index2;
        }
        else if (index2 == emptyIndex)
        {
            emptyIndex = index1;
        }

        CheckUnlock();
}

public void CheckUnlock()
{
    int unlockPos = 0;

    for (int i = 0; i < buttons.Length; i++)
    {
        int currentIndex = i;
        ArrowInitialData initialData = buttons[i].GetComponent<ArrowInitialData>();
        int initialIndex = initialData.initialIndex;

        // Check if a button on the left is on the right
        if (currentIndex > 3 && initialIndex + 4 == currentIndex )
        {
            unlockPos++;
        }

        // Check if a button on the right is on the left
        if (currentIndex < 3 && initialIndex - 4 == currentIndex)
        {
            unlockPos++;
        }
    }

    // If all buttons on the left are on the right and all buttons on the right are on the left, unlock the game
    if (unlockPos == 6)
    {
        Unlock();
    }
}


public void Unlock()
{
    currentState = LockState.UNLOCKED;
    UnlockEvent.Invoke();
}

public void Restart()
{
    // Reset each button to its initial position and index
    for (int i = 0; i < buttons.Length; i++)
    {
        ArrowInitialData initialData = buttons[i].GetComponent<ArrowInitialData>();
        int initialIndex = initialData.initialIndex;
        //Vector3 initialPosition = initialData.initialPosition;

        // If the button is not in its initial index, swap it with the button at the initial index
        if (i != initialIndex)
        {
            SwapButtons(i, initialIndex);
        }

        // Reset the button's position to its initial position
        //buttons[i].transform.position = initialData.initialPosition;
    }

    emptyIndex = 3;

}


}
