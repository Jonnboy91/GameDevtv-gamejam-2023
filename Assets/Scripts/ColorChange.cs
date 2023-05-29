using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorChange : MonoBehaviour
{
    private float _grey = -100f;
    private float _defaultColor = 0;

    ColorGrading _colorGrading;
    PostProcessVolume _postProcessVolume;
    Coroutine _coroutineRunning;


    void Start()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>(); 
        _colorGrading = _postProcessVolume.profile.GetSetting<ColorGrading>();
    }

    // Color to Grey
    private void ChangeColorToGrey()
    {
        _colorGrading.enabled.Override(true);
        if(_coroutineRunning == null)
            _coroutineRunning = StartCoroutine(ChangeColorRoutine(_defaultColor, _grey, 5f, true));
    }

    // Grey to Color
    private void ChangeGreyToColor()
    {
        _colorGrading.enabled.Override(true);
        if (_coroutineRunning == null)
            _coroutineRunning = StartCoroutine(ChangeColorRoutine(_grey, _defaultColor, 5f, false));
    }

    IEnumerator ChangeColorRoutine(float origin, float target, float duration, bool isTurningGrey)
    {
        float elapsedTime = 0f;
        float startTime = Time.realtimeSinceStartup;

        if (isTurningGrey)
        {
            while (elapsedTime < duration)
            {
                if (_colorGrading.saturation.value <= _grey)
                {
                    _coroutineRunning = null;
                    yield break;
                }
                else
                {
                    float lerpvalue = Mathf.Lerp(origin, target, Time.deltaTime  / duration );   
                    _colorGrading.saturation.value += (lerpvalue);      
                    elapsedTime = Time.realtimeSinceStartup - startTime;
                    yield return null;
                }
            }
        }
        else if (!isTurningGrey)
        {
            while (elapsedTime < duration)
            {
                if (_colorGrading.saturation.value >= _defaultColor)
                {
                    _coroutineRunning = null;
                    yield break;
                }
                else     
                {
                    float lerpvalue = Mathf.Lerp(target, origin, Time.deltaTime  / duration ) * - 1;   
                    _colorGrading.saturation.value += (lerpvalue);
                    elapsedTime = Time.realtimeSinceStartup - startTime;
                    yield return null;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_colorGrading.saturation.value >= 0)
        {
            ChangeColorToGrey();
        }
        else
        {
            ChangeGreyToColor();
        }
    }
}
