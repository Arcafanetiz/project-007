using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBehaviorTurnOn : MonoBehaviour
{
    [SerializeField] private Light2D lightSource;

    [Range(0.0f, 5.0f)] public float startIntensity;
    [Range(0.0f, 5.0f)] public float endIntensity;
    public float duration;

    public LeanTweenType easeType;

    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
        lightSource.intensity = startIntensity;
    }

    // Start is called before the first frame update
    void Start()
    {

        LeanTween.value(gameObject, UpdateIntensity, startIntensity, endIntensity, duration).setEase(easeType);

    }

    void UpdateIntensity(float val)
    {
        //Debug.Log("tweened value:" + val + " set this to whatever variable you are tweening...");
        lightSource.intensity = val;
    }
}
