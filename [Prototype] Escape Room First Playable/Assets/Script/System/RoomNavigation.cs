using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class RoomNavigation : MonoBehaviour
{
    public enum ViewType { STANDARD, AUXILLARY };
    public enum ViewState { PAST, PRESENT };

    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Light2D globalLightSource;
    [SerializeField] private int currentView;
    [SerializeField] private int previousView;
    [SerializeField] private int maxView;
    [SerializeField] private int currentAuxView;
    [SerializeField] private int previousAuxView;
    [SerializeField] private int maxAuxView;
    
    [Header("Views")]
    public KeyCode nextKey = KeyCode.E;
    public KeyCode prevKey = KeyCode.Q;
    [SerializeField] private ViewType currentType = ViewType.STANDARD;
    [SerializeField] private ViewState currentState = ViewState.PRESENT;

    public GameObject[] presentViews;
    public GameObject[] pastViews;

    public GameObject[] presentAuxViews;
    public GameObject[] pastAuxViews;

    public float[] viewGlobalLightIntensity;
    public float[] auxViewGlobalLightIntensity;

    [Header("Screen Effect")]
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

    public int p_currentAuxView
    {
        get { return currentAuxView; }
        set
        {
            if (value > maxAuxView - 1)
            {
                currentAuxView = 0;
            }
            else if (value < 0)
            {
                currentAuxView = maxAuxView - 1;
            }
            else
            {
                currentAuxView = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxView = presentViews.Length;
        currentView = 0;
        previousView = maxView - 1;

        maxAuxView = presentAuxViews.Length;
        currentAuxView = 0;
        previousAuxView = maxAuxView - 1;

        presentViews[0].SetActive(true);
        pastViews[0].SetActive(false);
        for (int i = 1; i < maxView; i++)
        {
            presentViews[i].SetActive(false);
            pastViews[i].SetActive(false);
        }

        for (int i = 0; i < presentAuxViews.Length; i++)
        {
            presentAuxViews[i].SetActive(false);
            pastAuxViews[i].SetActive(false);
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
        if(currentType == ViewType.STANDARD)
        {
            if(previousView == currentView)
            {
                return;
            }
            previousView = currentView;
            p_currentView += dir;
            switch (currentState)
            {
                case ViewState.PRESENT:
                    presentViews[previousView].SetActive(false);
                    presentViews[currentView].SetActive(true);
                    globalLightSource.intensity = viewGlobalLightIntensity[currentView];
                    break;
                case ViewState.PAST:
                    pastViews[previousView].SetActive(false);
                    pastViews[currentView].SetActive(true);
                    globalLightSource.intensity = viewGlobalLightIntensity[currentView];
                    break;
            }
        }
        if (currentType == ViewType.AUXILLARY)
        {
            if(previousAuxView == currentAuxView)
            {
                return;
            }
            previousAuxView = currentAuxView;
            p_currentAuxView += dir;
            switch (currentState)
            {
                case ViewState.PRESENT:
                    presentAuxViews[previousAuxView].SetActive(false);
                    presentAuxViews[currentAuxView].SetActive(true);
                    globalLightSource.intensity = auxViewGlobalLightIntensity[currentAuxView];
                    break;
                case ViewState.PAST:
                    pastAuxViews[previousAuxView].SetActive(false);
                    pastAuxViews[currentAuxView].SetActive(true);
                    globalLightSource.intensity = auxViewGlobalLightIntensity[currentAuxView];
                    break;
            }
        }
    }

    public void ChangeType()
    {
        if (currentType == ViewType.STANDARD)
        {
            switch (currentState)
            {
                case ViewState.PRESENT:
                    presentViews[currentView].SetActive(false);
                    presentAuxViews[0].SetActive(true);
                    globalLightSource.intensity = auxViewGlobalLightIntensity[currentAuxView];
                    break;
                case ViewState.PAST:
                    pastViews[currentView].SetActive(false);
                    pastAuxViews[0].SetActive(true);
                    globalLightSource.intensity = auxViewGlobalLightIntensity[currentAuxView];
                    break;
            }
            currentType = ViewType.AUXILLARY;
        }
        else if (currentType == ViewType.AUXILLARY)
        {
            switch (currentState)
            {
                case ViewState.PRESENT:
                    presentAuxViews[currentAuxView].SetActive(false);
                    presentViews[currentView].SetActive(true);
                    globalLightSource.intensity = viewGlobalLightIntensity[currentView];
                    break;
                case ViewState.PAST:
                    pastAuxViews[currentAuxView].SetActive(false);
                    pastViews[currentView].SetActive(true);
                    globalLightSource.intensity = viewGlobalLightIntensity[currentView];
                    break;
            }
            currentType = ViewType.STANDARD;
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
        if(currentType == ViewType.STANDARD)
        {
            switch (currentState)
            {
                case ViewState.PRESENT:
                    presentViews[currentView].SetActive(false);
                    pastViews[currentView].SetActive(true);
                    currentState = ViewState.PAST;
                    //AudioManager.instance.SwitchBGM(pastBGM);
                    LeanTween.value(gameObject, UpdateCutoffFreg, 22000.0f, 500.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    LeanTween.value(gameObject, UpdateReverbRoom, -10000.0f, 0.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    break;
                case ViewState.PAST:
                    pastViews[currentView].SetActive(false);
                    presentViews[currentView].SetActive(true);
                    currentState = ViewState.PRESENT;
                    //AudioManager.instance.SwitchBGM(presentBGM);
                    LeanTween.value(gameObject, UpdateCutoffFreg, 500.0f, 22000.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    LeanTween.value(gameObject, UpdateReverbRoom, 0.0f, -10000.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    break;
            }
        }
        if (currentType == ViewType.AUXILLARY)
        {
            switch (currentState)
            {
                case ViewState.PRESENT:
                    presentAuxViews[currentAuxView].SetActive(false);
                    pastAuxViews[currentAuxView].SetActive(true);
                    currentState = ViewState.PAST;
                    //AudioManager.instance.SwitchBGM(pastBGM);
                    LeanTween.value(gameObject, UpdateCutoffFreg, 22000.0f, 500.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    LeanTween.value(gameObject, UpdateReverbRoom, -10000.0f, 0.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    break;
                case ViewState.PAST:
                    pastAuxViews[currentAuxView].SetActive(false);
                    presentAuxViews[currentAuxView].SetActive(true);
                    currentState = ViewState.PRESENT;
                    //AudioManager.instance.SwitchBGM(presentBGM);
                    LeanTween.value(gameObject, UpdateCutoffFreg, 500.0f, 22000.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    LeanTween.value(gameObject, UpdateReverbRoom, 0.0f, -10000.0f, 0.7f).setEase(LeanTweenType.easeOutExpo);
                    break;
            }
        }
    }

    void UpdateCutoffFreg(float val)
    {
        audioMixer.SetFloat("bGMCutoffFreg", val);
    }

    void UpdateReverbRoom(float val)
    {
        audioMixer.SetFloat("bGMReverb", val);
    }
}
