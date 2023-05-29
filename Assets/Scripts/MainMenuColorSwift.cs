using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuColorSwift : MonoBehaviour
{
    
    private float grey = -100f;
    private float defaultColor = 0;

    private float maxIntensity = 1;
    private float defaultIntensity = 0;


    ColorGrading colorGrading;
    PostProcessVolume postProcessVolume;
    Vignette vignette;

    Coroutine coroutineRunning;
    Coroutine coroutine2Running;


    private void Awake() {
        postProcessVolume = GetComponent<PostProcessVolume>(); 
        colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
        vignette = postProcessVolume.profile.GetSetting<Vignette>();
    }

    void Start()
    {
        ChangeColorToGrey();
    }

    // Color to Grey
    private void ChangeColorToGrey()
    {
        colorGrading.enabled.Override(true);
        vignette.enabled.Override(true);
        if(coroutineRunning == null)
            coroutineRunning = StartCoroutine(ChangeColorRoutine(defaultColor, grey, 10f));
        if(coroutine2Running == null)
            coroutine2Running = StartCoroutine(ChangeVignetteIntensity(defaultIntensity, maxIntensity, 10f));
    }

    IEnumerator ChangeColorRoutine(float origin, float target, float duration)
    {
        float startTime = Time.realtimeSinceStartup;
        float endTime = startTime + duration;

        while (Time.realtimeSinceStartup < endTime)
        {
            float t = (Time.realtimeSinceStartup - startTime) / duration;
            float lerpValue = Mathf.Lerp(origin, target, t);
            colorGrading.saturation.value = lerpValue;
            yield return null;
        }

        coroutineRunning = null;
    }

    IEnumerator ChangeVignetteIntensity(float origin, float target, float duration)
    {
        float startTime = Time.realtimeSinceStartup;
        float endTime = startTime + duration;

        while (Time.realtimeSinceStartup < endTime)
        {
            float t = (Time.realtimeSinceStartup - startTime) / duration;
            float lerpValue = Mathf.Lerp(origin, target, t);
            vignette.intensity.value = lerpValue;
            yield return null;
        }

        coroutine2Running = null;
    }
}
