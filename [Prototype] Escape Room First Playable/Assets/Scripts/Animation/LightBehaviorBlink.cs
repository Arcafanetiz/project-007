using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBehaviorBlink : MonoBehaviour
{
    [SerializeField] private Light2D lightSource;
    [Range(0.0f, 5.0f)] public float minIntensity;
    [Range(0.0f, 5.0f)] public float maxIntensity;
    public float blinkDuration;
    public float blinkDelay;

    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
        lightSource.intensity = minIntensity;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Fade in
            LeanTween.value(gameObject, UpdateIntensity, minIntensity, maxIntensity, blinkDuration / 2f)
                .setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(blinkDuration / 2f);

            // Fade out
            LeanTween.value(gameObject, UpdateIntensity, maxIntensity, minIntensity, blinkDuration / 2f)
                .setEase(LeanTweenType.easeInOutSine);

             yield return new WaitForSeconds(blinkDuration / 2f);

            // Fade in
             LeanTween.value(gameObject, UpdateIntensity, minIntensity, maxIntensity, blinkDuration / 2f)
                .setEase(LeanTweenType.easeInOutSine);

            yield return new WaitForSeconds(blinkDuration / 2f);

            // Fade out
            LeanTween.value(gameObject, UpdateIntensity, maxIntensity, minIntensity, blinkDuration / 2f)
                .setEase(LeanTweenType.easeInOutSine);


            yield return new WaitForSeconds(blinkDelay);
        }
    }

    void UpdateIntensity(float val)
    {
        lightSource.intensity = val;
    }
}
