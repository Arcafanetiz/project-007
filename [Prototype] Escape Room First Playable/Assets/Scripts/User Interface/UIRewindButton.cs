using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class UIRewindButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    [Header("Private Serialized Field -Do not touch-")]
    public bool onCD = false;
    private float cDTimer;

    [Header("Hold Settings")]
    public float requiredHoldTime;
    public float cDTime;

    [Header("Button Events")]
    public UnityEvent OnHoldClick;

    [Header("Gauge Animation")]
    public bool isRewinded = false;
    public bool flip = true;
    public float fillSize;
    private float startGauge;
    private float endGauge = 1.0f;
    private int dir = -1;
    [SerializeField] private Image filledImage;

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
            AudioManager.instance.PlayAudio("UI Clock Ticking");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!onCD)
            {
                pointerDown = true;
                AudioManager.instance.PlayAudio("UI Clock Ticking");
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Reset();
        }

        if (pointerDown && !onCD)
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
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        filledImage.fillAmount = startGauge + (float)(pointerDownTimer / requiredHoldTime * (endGauge - startGauge));
        AudioManager.instance.StopAudio("UI Clock Ticking");
    }

    IEnumerator StartCoolDown()
    {
        if (!isRewinded)
        {
            AudioManager.instance.PlayAudio("UI Whoosh(1.2)");
        }
        else
        {
            AudioManager.instance.PlayAudio("UI Whoosh(0.8)");
        }
        yield return new WaitForSeconds(cDTime);
        onCD = false;
    }
}
