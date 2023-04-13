using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private GameObject FadeIn;
    [SerializeField] private GameObject FadeOut;

    [Header("Audio Settings")]
    public bool changeBGM;
    public string bGMName;

    void Start()
    {
        FadeIn.SetActive(true);
        FadeOut.SetActive(false);
    }

    public void LoadScene(int scene_index)
    {
        FadeOut.SetActive(true);
        StartCoroutine(WaitForTransition(scene_index)); 
    }

    private IEnumerator WaitForTransition(int scene_index)
    {
        while(FadeOut.GetComponent<GDTFadeEffect>().HasFinished())
        {
            yield return null;
        }

        SceneManager.LoadScene(scene_index);

        if(changeBGM)
        {
            AudioManager.instance.SwitchBGM(bGMName);
        }

        //AsyncOperation operation = SceneManager.LoadSceneAsync(scene_index);
        //while (!operation.isDone)
        //{
        //    float progress = Mathf.Clamp01(operation.progress / 0.9f);
        //    Debug.Log(progress);
        //    yield return null;
        //}
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
