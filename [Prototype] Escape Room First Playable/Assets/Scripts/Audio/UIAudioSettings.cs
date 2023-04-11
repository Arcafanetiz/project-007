using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSettings : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bGMSlider;
    [SerializeField] private Slider sFXSlider;

    public void SetVolume()
    {
        AudioManager.instance.SetBGMVolume(masterSlider.value * bGMSlider.value);
        AudioManager.instance.SetSFXVolume(masterSlider.value * sFXSlider.value);
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
