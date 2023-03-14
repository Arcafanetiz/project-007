using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
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
        StartCoroutine(WaitForTransition()); 
    }

    private IEnumerator WaitForTransition()
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
