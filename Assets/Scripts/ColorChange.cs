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
            _coroutineRunning = StartCoroutine(ChangeColorRoutine(_defaultColor, _grey, 500f, true));
    }

    // Grey to Color
    private void ChangeColorToColor()
    {
        _colorGrading.enabled.Override(true);
        if (_coroutineRunning == null)
            _coroutineRunning = StartCoroutine(ChangeColorRoutine(_grey, _defaultColor, 200f, false));
    }

    IEnumerator ChangeColorRoutine(float origin, float target, float duration, bool isTurningGrey)
    {
        float time = 0;

        if (isTurningGrey)
        {
            while (time < duration)
            {
                if (_colorGrading.saturation.value < _grey)
                {
                    _coroutineRunning = null;
                    yield break;
                }
                else
                {
                    float lerpvalue = Mathf.Lerp(origin, target, time / duration);   
                    _colorGrading.saturation.value += (lerpvalue);      
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }
        else if (!isTurningGrey)
        {
            while (time < duration)
            {
                if (_colorGrading.saturation.value >= _defaultColor)      // IF - _defaultValue >= 0, DO NOTHING (CORRECT)
                {
                    _coroutineRunning = null;
                    yield break;
                }
                else     
                {
                    float _multiplier = -0.005f;
                    float lerpvalue = Mathf.Lerp(origin, target, time / duration);
                    _colorGrading.saturation.value += (lerpvalue * _multiplier);   
                    time += Time.deltaTime;
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
            ChangeColorToColor();
        }
    }
}
