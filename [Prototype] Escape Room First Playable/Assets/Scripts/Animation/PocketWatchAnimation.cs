using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class PocketWatchAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator clockAnim;

    //private bool isRewinded = false;

    private UIHoldButton button;

    void Start(){
        button = GetComponent<UIHoldButton>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!button.isRewinded)
        {
             clockAnim.SetBool("isRewind",true);
             //isRewinded = true;
            
        }
         if(button.isRewinded)
        {
             clockAnim.SetBool("isUpwind",true);
             //clockAnim.speed = -1.0f;
             //isRewinded = false;
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        clockAnim.SetBool("isRewind",false);
        clockAnim.SetBool("isUpwind",false);
        //audioSourceLoad.Stop();
    }
}




