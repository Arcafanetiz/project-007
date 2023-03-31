using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public GameObject FadeIn;
    public GameObject FadeOut;
    private string SceneName;

    void Start()
    {
        FadeOut.SetActive(false);
        FadeIn.SetActive(true);
    }

    public void LoadScene(string scenename)
    {
        SceneName = scenename;
        FadeOut.SetActive(true);
        StartCoroutine(WaitForTransition(0)); 
    }

    private IEnumerator WaitForTransition(int index)
    {
        while (FadeOut.GetComponent<GDTFadeEffect>().HasFinished())
        {
            yield return null;
        }
        Debug.Log("Load scene: " + SceneName);
        SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }       
}
