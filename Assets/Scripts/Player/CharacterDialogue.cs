using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    private int _currentStringIndex = 0;
    private int _currentCutsceneIndex = 0;

    private char _blankSpace = ' ';

    private List<string> _dialogueOne = new List<string>();
    private List<string> _dialogueTwo = new List<string>();
    private List<string> _dialogueThree = new List<string>();

    private WaitForSeconds _blankFinalTime = new WaitForSeconds(2.5f);
    private WaitForSeconds _textIntroRemoveDelay = new WaitForSeconds(1.25f);
    private WaitForSeconds _textDelayTime = new WaitForSeconds(0.05f);
    private Coroutine _cutsceneRoutine;


    void Start()
    {
        Dialogue();
        PlayCutsceneRoutine();
    }

    // Method to hold all the dialogue. Make sure to initialise in Start()
    void Dialogue()
    {
        _dialogueOne[0] = "This is a story about 2 dads on a journey to being the best game developers in the World...";
        _dialogueTwo[0] = "Hello...this is a story about...";
        _dialogueThree[0] = "Hello...this is a story about...";
    }

    // 
    void PlayCutsceneRoutine()
    {
        if (_cutsceneRoutine == null)       // Make sure only 1 can be played at a time
        {
            switch (_currentCutsceneIndex)
            {
                case 0:
                    _cutsceneRoutine = StartCoroutine(CutsceneRoutine(_dialogueOne));    // Play dialogue for cutscene 1
                    break;
                case 1:
                    _cutsceneRoutine = StartCoroutine(CutsceneRoutine(_dialogueTwo));    // Play dialogue for cutscene 2
                    break;
                case 2:
                    _cutsceneRoutine = StartCoroutine(CutsceneRoutine(_dialogueThree));  // Play dialogue for cutscene 3
                    break;
                default:
                    Debug.Log("No valid cutscene index provided");
                    break;
            }
        }
    }

    // Cutscene Dialogue Routine
    IEnumerator CutsceneRoutine(List<string> dialogue)
    {
        while (_currentStringIndex < dialogue.Count)
        {
            foreach (char letter in dialogue[_currentStringIndex].ToCharArray())
            {
                UIManager.Instance.UpdateDialogueTextDisplay(letter);
                yield return _textDelayTime;
            }

            _currentCutsceneIndex++;
            yield return _textIntroRemoveDelay;
            UIManager.Instance.UpdateDialogueTextDisplay(_blankSpace);
            yield return _blankFinalTime;

            _currentStringIndex = 0;
            _cutsceneRoutine = null;
        }
    }
}
