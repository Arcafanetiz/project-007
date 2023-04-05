using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [Header("Sound Settings")]
    public string name;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)] public float volume = 1.0f;
    [Range(0.1f, 3.0f)] public float pitch = 1.0f;
    public bool loop;

    public float currentVolume;
    [HideInInspector] public AudioSource source;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
