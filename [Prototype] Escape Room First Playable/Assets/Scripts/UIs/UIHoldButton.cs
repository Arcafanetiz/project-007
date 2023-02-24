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

    private bool onCD = false;
    private float cDTimer;

    public float requiredHoldTime;
    public float cDTime;

    public UnityEvent OnHoldClick;

    [SerializeField] private Image filledImage;

    public void Awake()
    {
        Reset();
        cDTimer = cDTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
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
                    StartCoroutine(StartCoolDown());
                }
                Reset();
            }
            filledImage.fillAmount = (float)(0.75 + (pointerDownTimer / requiredHoldTime * 0.25));
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Reset();
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        filledImage.fillAmount = (float)(0.75 + (pointerDownTimer / requiredHoldTime * 0.25));
    }

    IEnumerator StartCoolDown()
    {
        yield return new WaitForSeconds(cDTime);
        onCD = false;
    }
}
