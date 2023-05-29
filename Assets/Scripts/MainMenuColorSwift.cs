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
        float elapsedTime = 0f;
        float startTime = Time.realtimeSinceStartup;

        while (elapsedTime < duration)
        {
            if (colorGrading.saturation.value <= grey){
                    coroutineRunning = null;
                    yield break;
            }
        
            float lerpvalue = Mathf.Lerp(origin, target, Time.deltaTime  / duration );   
            colorGrading.saturation.value += (lerpvalue);    
            elapsedTime = Time.realtimeSinceStartup - startTime; 
            yield return null;
        }
        coroutineRunning = null;
    }

    IEnumerator ChangeVignetteIntensity(float origin, float target, float duration)
    {
        float elapsedTime = 0f;
        float startTime = Time.realtimeSinceStartup;

        while (elapsedTime < duration)
        {
            if (vignette.intensity.value >= maxIntensity){
                    coroutine2Running = null;
                    yield break;
            }

            float lerpvalue = Mathf.Lerp(origin, target, Time.deltaTime  / duration );  
            vignette.intensity.value += lerpvalue;
            elapsedTime = Time.realtimeSinceStartup - startTime;
            yield return null;
        }
        coroutine2Running = null;
    }
}
