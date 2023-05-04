using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DAnimation : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Light2D lightSource;

    [Header("Animation - Turn On")]
    public bool turnOnEnable;
    [Range(0.0f, 5.0f)] public float startIntensity;
    [Range(0.0f, 5.0f)] public float endIntensity;
    public float duration;
    public LeanTweenType easeType;
    public bool turnOnSound;
    public string turnOnAudio = "Lights On";
    [Header("Animation - Flicker")]
    public bool flickIntensity;
    float _baseIntensity;
    public float intensityRange;
    public float intensityTimeMin;
    public float intensityTimeMax;
    [Header("Animation - Sway")]
    public bool lightSway;
    public float swayIntensity;
    public float swayDuration;
    private float originRot;

    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
        lightSource.intensity = startIntensity;
        _baseIntensity = endIntensity;
    }

    // Start is called before the first frame update
    void Start()
    {
        originRot = gameObject.transform.rotation.eulerAngles.z;
        LeanTween.value(gameObject, UpdateIntensity, startIntensity, endIntensity, duration).setEase(easeType);
        StartCoroutine(FlickIntensity());
        LightSway();
        if (turnOnSound)
            AudioManager.instance.PlayAudio(turnOnAudio);
    }

    private void OnEnable()
    {
        if(turnOnEnable)
            LeanTween.value(gameObject, UpdateIntensity, startIntensity, endIntensity, duration).setEase(easeType);
    }

    void UpdateIntensity(float val)
    {
        lightSource.intensity = val;
    }

    void LightSway()
    {
        if(lightSway)
        {
            LeanTween.rotate(gameObject, new Vector3(0.0f, 0.0f, originRot + swayIntensity) , swayDuration).setEase(LeanTweenType.easeShake).setLoopPingPong();
        }
    }

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickIntensity)
            {
                t0 = Time.time;
                float r = Random.Range(_baseIntensity - intensityRange, _baseIntensity + intensityRange);
                lightSource.intensity = r;
                t = Random.Range(intensityTimeMin, intensityTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
}
