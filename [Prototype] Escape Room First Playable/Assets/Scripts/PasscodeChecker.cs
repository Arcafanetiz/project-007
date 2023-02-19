using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PasscodeChecker : MonoBehaviour
{
    [SerializeField] private TMP_Text Ans;

    public string Answer = "1234";

    // Start is called before the first frame update
    private void Start()
    {
        Ans.text = "";
    }
    public void Number(int num)
    {
        Ans.text += num.ToString();
    }

    public void Execute()
    {
        if(Ans.text == Answer)
        {
            Ans.text = "Correct!!!";
        }
        else
        {
            StartCoroutine(Incorrect());
        }
    }

    private IEnumerator Incorrect()
    {
        Ans.text = "INCORRECT!!";
        yield return new WaitForSeconds(2.0f);
        Ans.text = "";
    }

    
}
