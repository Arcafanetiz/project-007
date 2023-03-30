using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class UIHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    public bool onCD = false;
    private float cDTimer;

    public float requiredHoldTime;
    public float cDTime;

    public UnityEvent OnHoldClick;
    public bool isRewinded = false;

    public bool flip = true;
    public float fillSize;
    private float startGauge;
    private float endGauge = 1.0f;
    private int dir = -1;
    [SerializeField] private Image filledImage;

    public AudioSource audioSourceLoad;
    public AudioSource audioSourceExecute;

    public void Awake()
    {
        cDTimer = cDTime;
        startGauge = 1.0f - fillSize;
        Reset();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!onCD)
        {
            pointerDown = true;
            audioSourceLoad.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown || Input.GetKey(KeyCode.LeftControl) && !onCD)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                if (OnHoldClick != null)
                {
                    OnHoldClick.Invoke();
                    onCD = true;
                    isRewinded = !isRewinded;
                    if (flip)
                    {
                        startGauge += (dir * (1.0f - fillSize));
                        endGauge += (dir * (1.0f - fillSize));
                        filledImage.fillClockwise = !filledImage.fillClockwise;
                        dir *= -1;
                    }
                    StartCoroutine(StartCoolDown());
                }
                Reset();
            }
            filledImage.fillAmount = startGauge + (float)(pointerDownTimer / requiredHoldTime * (endGauge - startGauge));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !onCD)
        {
            audioSourceLoad.Play();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Reset();
            audioSourceLoad.Stop();
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        filledImage.fillAmount = startGauge + (float)(pointerDownTimer / requiredHoldTime * (endGauge - startGauge));
        audioSourceLoad.Stop();
    }

    IEnumerator StartCoolDown()
    {
        if (!isRewinded)
        {
            audioSourceExecute.pitch = 1.2f;
        }
        else
        {
            audioSourceExecute.pitch = 0.8f;
        }
        audioSourceExecute.Play();
        yield return new WaitForSeconds(cDTime);
        onCD = false;
    }
}
