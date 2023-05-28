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
            coroutineRunning = StartCoroutine(ChangeColorRoutine(defaultColor, grey, 40000f));
        if(coroutine2Running == null)
            coroutine2Running = StartCoroutine(ChangeVignetteIntensity(defaultIntensity, maxIntensity, 40000f));
    }

    IEnumerator ChangeColorRoutine(float origin, float target, float duration)
    {
        float time = 0;

            while (time < duration)
            {
                if (colorGrading.saturation.value <= target)
                {
                    coroutineRunning = null;
                    yield break;
                }
                else
                {
                    float lerpvalue = Mathf.Lerp(origin, target, time / duration);   
                    colorGrading.saturation.value += (lerpvalue);      
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }

        IEnumerator ChangeVignetteIntensity(float origin, float target, float duration)
    {
        float time = 0;

            while (time < duration)
            {
                if (vignette.intensity.value >= target)
                {
                    coroutineRunning = null;
                    yield break;
                }
                else
                {
                    float lerpvalue = Mathf.Lerp(origin, target, time / duration);   
                    vignette.intensity.value += (lerpvalue);      
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }
}
