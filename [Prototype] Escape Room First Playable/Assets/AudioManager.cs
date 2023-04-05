using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private GameObject BGMSources;
    [SerializeField] private GameObject SFXSources;

    [Header("Sound Manager Settings")]
    public string _BGMName;
    public bool playBGM;
    public Sound[] BGMSounds;
    public Sound[] SFXSounds;

    [HideInInspector] public AudioSource BGMSource;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        BGMSource = BGMSources.AddComponent<AudioSource>();

        foreach (Sound s in BGMSounds)
        {
            s.source = BGMSource;

            s.currentVolume = s.volume;
        }

        foreach (Sound s in SFXSounds)
        {
            s.source = SFXSources.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.currentVolume = s.volume;
        }
    }

    public void SwitchBGM(string name)
    {
        // ---------------- Calling Method ----------------
        // FindObjectOfType<AudioManager>().SwitchBGM("name");
        // AudioManager.instance.SwitchBGM("name");
        // AudioManager.instance.BGMSource.Stop();
        // ------------------------------------------------

        BGMSource.Stop();
        Sound s = Array.Find(BGMSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        BGMSource.clip = s.audioClip;
        BGMSource.volume = s.volume;
        BGMSource.pitch = s.pitch;
        BGMSource.loop = s.loop;
        BGMSource.Play();
    }

    public void PlayAudio(string name)
    {
        // ---------------- Calling Method ----------------
        // FindObjectOfType<AudioManager>().PlayAudio("name");
        // ------------------------------------------------

        Sound s = Array.Find(SFXSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopAudio(string name)
    {
        // ---------------- Calling Method ----------------
        // FindObjectOfType<AudioManager>().StopAudio("name");
        // ------------------------------------------------

        Sound s = Array.Find(SFXSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void ToggleBGM()
    {
        BGMSource.mute = !BGMSource.mute;
    }

    public void SetBGMVolume(float volume)
    {
        BGMSource.volume = volume;
    }

    public void ToggleSFX()
    {
        foreach (Sound s in SFXSounds)
        {
            s.source.mute = !s.source.mute;
        }
    }

    public void SetSFXVolume(float volume)
    {
        foreach (Sound s in SFXSounds)
        {
            s.source.volume = s.volume;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(playBGM)
        {
            SwitchBGM(_BGMName);
        } 
    }

    // Update is called once per frame
    void Update() { }
}
