using System.Collections;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    private string[] _dialogueOne = new string[9];
    private string[] _dialogueTwo = new string[1];
    private string[] _dialogueThree = new string[1];
    private char _blankSpace = ' ';

    private string name = "Jacob";

    private int _currentStringIndex = 0;
    private int _currentCutsceneIndex = 0;

    private WaitForSeconds _blankFinalTime = new WaitForSeconds(1.0f);
    private WaitForSeconds _clearTextBoxDelay = new WaitForSeconds(1f);
    private WaitForSeconds _textDelayTime = new WaitForSeconds(0.05f);
    private Coroutine _cutsceneRoutine;


    void Start()
    {
        DialogueCutsceneOne();
        DialogueCutsceneTwo();
        DialogueCutsceneThree();
    }

    // Method to hold all the dialogue. Make sure to initialise in Start()
    void DialogueCutsceneOne()
    {
        _dialogueOne[0] = $"{name} was a happy boy...";
        _dialogueOne[1] = "He enjoyed playing in the forest around him, chasing butterflies and frogs until his mother called him in for dinner...";
        _dialogueOne[2] = "He enjoyed the fresh forest smell oozing from the pine trees and the blossoming flowers when Spring began...";
        _dialogueOne[3] = "He enjoyed the sounds of the busy bees buzzing around him, and the birds chattering in the trees...";
        _dialogueOne[4] = "One of his favourite hobbies was jumping in his boat by the lake and waiting patiently until dusk when the fish started nibbling...";
        _dialogueOne[5] = "But as time went by and he started to grow up, he found himself turning into a stranger...";
        _dialogueOne[6] = "Always angry, frustrated and at times often sad and lonely.";
        _dialogueOne[7] = "Overtime this got worse, as the weight of turning into an adult and the responsibilities with it, fell heavily on his shoulders...";
        _dialogueOne[8] = $"Overwhelmed with emotions, {name} decided to seek help and bring back his fun, happy and energetic self";

    }

    void DialogueCutsceneTwo()
    {
        _dialogueTwo[0] = "Hello...this is a story about...";
    }

    void DialogueCutsceneThree()
    {
        _dialogueThree[0] = "Hello...this is a story about...";
    }

    // 
    public void PlayCutsceneRoutine()
    {
        if (_cutsceneRoutine == null)       // Make sure only 1 can be played at a time.
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
    IEnumerator CutsceneRoutine(string[] dialogue)
    {
        while (_currentStringIndex < dialogue.Length)
        {
            foreach (char letter in dialogue[_currentStringIndex].ToCharArray())
            {
                UIManager.Instance.UpdateDialogueTextDisplay(letter);
                yield return _textDelayTime;
            }

            _currentStringIndex++;
            _currentCutsceneIndex++;
            yield return _clearTextBoxDelay;
            UIManager.Instance.ClearTextBox();
            UIManager.Instance.UpdateDialogueTextDisplay(_blankSpace);
            yield return _blankFinalTime;
        }

        _cutsceneRoutine = null;
    }
}
