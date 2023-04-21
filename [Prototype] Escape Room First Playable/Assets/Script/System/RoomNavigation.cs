using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public enum ViewState { PAST, PRESENT };

    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private int currentView;
    [SerializeField] private int previousView;
    [SerializeField] private int maxView;

    [Header("Views")]
    public KeyCode nextKey = KeyCode.E;
    public KeyCode prevKey = KeyCode.Q;
    [SerializeField] private ViewState currentState = ViewState.PRESENT;

    public GameObject[] presentViews;
    public GameObject[] pastViews;

    [Header("Audio")]
    public string presentBGM;
    public string pastBGM;

    public GameObject screenEffect;
    public int p_currentView
    {
        get { return currentView; }
        set
        {
            if (value > maxView - 1)
            {
                currentView = 0;
            }
            else if (value < 0)
            {
                currentView = maxView - 1;
            }
            else
            {
                currentView = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxView = presentViews.Length;
        currentView = 0;
        previousView = maxView;

        presentViews[0].SetActive(true);
        pastViews[0].SetActive(false);
        for (int i = 1; i < maxView; i++)
        {
            presentViews[i].SetActive(false);
            pastViews[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextKey))
        {
            ChangeView(1);
        }
        if (Input.GetKeyDown(prevKey))
        {
            ChangeView(-1);
        }
    }

    public void ChangeView(int dir)
    {
        previousView = currentView;
        p_currentView += dir;
        switch(currentState)
        {
            case ViewState.PRESENT:
                presentViews[previousView].SetActive(false);
                presentViews[currentView].SetActive(true);
                break;
            case ViewState.PAST:
                pastViews[previousView].SetActive(false);
                pastViews[currentView].SetActive(true);
                break;
        }
        
    }

    public void ToggleState()
    {
        StartCoroutine(ChangeState());
    }

    IEnumerator ChangeState()
    {
        LeanTween.cancel(screenEffect);
        screenEffect.transform.localScale = Vector3.one;
        LeanTween.scale(screenEffect, new Vector3(900.0f, 900.0f, 1.0f), 1.0f).setOnComplete(() 
            => { screenEffect.transform.localScale = Vector3.one; });
        yield return new WaitForSecondsRealtime(0.2f);
        switch (currentState)
        {
            case ViewState.PRESENT:
                presentViews[currentView].SetActive(false);
                pastViews[currentView].SetActive(true);
                currentState = ViewState.PAST;
                AudioManager.instance.SwitchBGM(pastBGM);
                break;
            case ViewState.PAST:
                pastViews[currentView].SetActive(false);
                presentViews[currentView].SetActive(true);
                currentState = ViewState.PRESENT;
                AudioManager.instance.SwitchBGM(presentBGM);
                break;
        }
    }
}
