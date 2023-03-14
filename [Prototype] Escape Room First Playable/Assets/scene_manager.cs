using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    public GameObject TransitionEffect;
    private string SceneName;

    void Start()
    {
        TransitionEffect.SetActive(false);
    }
    public void LoadScene(string scenename)
    {
        SceneName = scenename;
        TransitionEffect.SetActive(true);
        StartCoroutine(WaitForTransition()); 
    }

    private IEnumerator WaitForTransition()
    {
        while (TransitionEffect.GetComponent<GDTFadeEffect>().HasFinished())
        {
            yield return null;
        }
        Debug.Log("Load scene: " + SceneName);
        SceneManager.LoadScene(SceneName);
    }
}
