using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private PlayableDirector _playableDirector;


    private void Awake()
    {
        _instance = this;
    }


    private void Start()
    {
        _dialogueText = GameObject.Find("Dialogue Text").GetComponent<TextMeshProUGUI>();
        _playableDirector = GameObject.Find("Timeline Director Manager").GetComponent<PlayableDirector>();
        StartCoroutine(PlayDirectorRoutine());
    }

    public void UpdateDialogueTextDisplay(char letter)
    {
        _dialogueText.text += letter;
    }

    public void ClearTextBox()
    {
        _dialogueText.text = string.Empty;
    }

    IEnumerator PlayDirectorRoutine()
    {
        Debug.Log(Time.time.ToString());
        yield return new WaitForSeconds(2f);
        Debug.Log(Time.time.ToString());
        _playableDirector.Play();
    }
}
