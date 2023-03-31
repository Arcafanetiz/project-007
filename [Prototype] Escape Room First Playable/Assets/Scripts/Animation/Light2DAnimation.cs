using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DAnimation : MonoBehaviour
{
    [Header("Private Serialized Field -Do not touch-")]
    [SerializeField] private Light2D lightSource;

    [Header("Animation - Turn On")]
    [Range(0.0f, 5.0f)] public float startIntensity;
    [Range(0.0f, 5.0f)] public float endIntensity;
    public float duration;
    public LeanTweenType easeType;
    [Header("Animation - Flicker")]
    public bool flickIntensity;
    float _baseIntensity;
    public float intensityRange;
    public float intensityTimeMin;
    public float intensityTimeMax;


    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
        lightSource.intensity = startIntensity;
        _baseIntensity = endIntensity;
    }

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.value(gameObject, UpdateIntensity, startIntensity, endIntensity, duration).setEase(easeType);
        StartCoroutine(FlickIntensity());
    }

    void UpdateIntensity(float val)
    {
        lightSource.intensity = val;
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
