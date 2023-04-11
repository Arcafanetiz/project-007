using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRewindButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Animator animator;

    private UIRewindButton rewindButton;

    void Start()
    {
        rewindButton = GetComponent<UIRewindButton>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!rewindButton.isRewinded && !rewindButton.onCD)
        {
            animator.SetBool("isRewind", true);
        }
        if(rewindButton.isRewinded && !rewindButton.onCD)
        {
            animator.SetBool("isUpwind", true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetBool("isRewind",false);
        animator.SetBool("isUpwind",false);
    }
}




