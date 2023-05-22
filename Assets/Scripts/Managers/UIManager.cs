using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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




    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _dialogueText = GameObject.Find("Dialogue Text").GetComponent<TextMeshProUGUI>();
        if (_dialogueText == null)
            Debug.Log("Couldn't find TEXTMESHPROUGUI component on UIManager");
    }

    public void UpdateDialogueTextDisplay(char letter)
    {
        _dialogueText.text += letter;
    }
}
