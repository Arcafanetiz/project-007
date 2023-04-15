using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIAudioSettings : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bGMSlider;
    [SerializeField] private Slider sFXSlider;
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume()
    {
        float volume = bGMSlider.value;
        audioMixer.SetFloat("bGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sFXSlider.value;
        audioMixer.SetFloat("sFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void PlaySampleSFX()
    {
        AudioManager.instance.PlayAudio("Water Splash");
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        if (PlayerPrefs.HasKey("BGMVolume"))
            bGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        if (PlayerPrefs.HasKey("SFXVolume"))
            sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadVolume();

        SetMasterVolume();
        SetBGMVolume();
        SetSFXVolume();
    }

    // Update is called once per frame
    void Update() { }
}
