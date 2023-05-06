using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectWhenAnimationDone : MonoBehaviour
{
 public GameObject gameObjectToActivate;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the animation has finished playing
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // Activate the game object
            gameObjectToActivate.SetActive(true);
        }
    }
}
