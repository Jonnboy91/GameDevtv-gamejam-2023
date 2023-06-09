using System.Collections;
using UnityEngine;
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

    private void Awake()
    {
        _instance = this;
    }


    private void Start()
    {
        _dialogueText = GameObject.Find("Dialogue Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDialogueTextDisplay(char letter)
    {
        _dialogueText.text += letter;
    }

    public void ClearTextBox()
    {
        _dialogueText.text = string.Empty;
    }
}
