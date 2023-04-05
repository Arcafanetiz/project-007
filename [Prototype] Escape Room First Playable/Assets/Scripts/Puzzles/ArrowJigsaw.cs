using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowJigsaw : MonoBehaviour
{
    public Button[] buttons;
    public int emptyIndex = 3;
    private Vector3[] initialPositions;
    private int[] initialIndices;
    
    // Start is called before the first frame update
    void Start()
    {
         // Store the initial positions of the buttons and the initial value of emptyIndex
        initialPositions = new Vector3[buttons.Length];
        initialIndices = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            initialPositions[i] = buttons[i].transform.position;
            initialIndices[i] = i;
        }
        
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
}
public bool CheckButtonPositions()
{
    int leftmostLeftButtonIndex = int.MaxValue;
    int rightmostRightButtonIndex = int.MinValue;

    // Find the leftmost index of buttons that moved left and the rightmost index of buttons that moved right
    for (int i = 0; i < buttons.Length; i++)
    {
        if (initialIndices[i] > emptyIndex) // button moved left
        {
            if (initialIndices[i] < leftmostLeftButtonIndex)
            {
                leftmostLeftButtonIndex = initialIndices[i];
            }
        }
        else if (initialIndices[i] < emptyIndex) // button moved right
        {
            if (initialIndices[i] > rightmostRightButtonIndex)
            {
                rightmostRightButtonIndex = initialIndices[i];
            }
        }
    }

    // Check if all buttons that moved left are to the left of all buttons that moved right
    return leftmostLeftButtonIndex < rightmostRightButtonIndex;
}
public void Restart()
{
    
     // Set the positions of the buttons back to their initial positions
    for (int i = 0; i < buttons.Length; i++)
    {
        buttons[i].transform.position = initialPositions[i];
    }
    
    // Set the indices of the buttons back to their initial indices
    for (int i = 0; i < buttons.Length; i++)
    {
        Button button = buttons[i];
        int index = initialIndices[i];
        buttons[index] = button;
        button.transform.SetSiblingIndex(index);
    }
    
    // Reset the empty index to its initial value
    emptyIndex = 3;
}

}
