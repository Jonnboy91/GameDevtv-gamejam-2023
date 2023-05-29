using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDialogue : MonoBehaviour
{
    private string[] _dialogueOne = new string[8];
    private string[] _dialogueTwo = new string[2];
    private string[] _dialogueThree = new string[1];

    private string _firstName = "Jacob";

    private int _currentStringIndex = 0;
    private int _currentCutsceneIndex = 0;

    private WaitForSecondsRealtime _clearTextBoxDelay = new WaitForSecondsRealtime(1.25f);
    private WaitForSecondsRealtime _textDelayTime = new WaitForSecondsRealtime(0.05f);
    private Coroutine _cutsceneRoutine;


    void Start()
    {
        CheckScene();
        _currentStringIndex = 0;
        DialogueCutsceneOne();
        DialogueCutsceneTwo();
        DialogueCutsceneThree();
        Invoke(nameof(PlayCutsceneRoutine), 2f);
    }

    void CheckScene()
    {
        int result = SceneManager.GetActiveScene().buildIndex;

        switch (result)
        {
            case 0:
            case 1:
                _currentCutsceneIndex = 0;
                break;
            case 2:
            case 3:
                _currentCutsceneIndex = 1;
                break;
            case 4:
            case 5:
                _currentCutsceneIndex = 2;
                break;
            default:
                Debug.Log("Incorect build index number");
                break;
        }
    }

    // Method to hold all the dialogue. Make sure to initialise in Start()
    // Cutscene 1 - As Player walks from home to the therapists office he is overcome with his demons and must stop and fight them on the way. Turns grey.
    void DialogueCutsceneOne()
    {
        _dialogueOne[0] = $"Everyone thought {_firstName} was a happy man...";
        _dialogueOne[1] = $"But the reality was that he had struggled for most of his life.";
        _dialogueOne[2] = $"Teenage issues, relationships, work, depression, anxiety and even death were all weighing heavily on his shoulders.";
        _dialogueOne[3] = $"Everytime he stepped out of his house he put on a brave face, hiding his real feelings in plain sight.";
        _dialogueOne[4] = $"But, as time went by, he started to experience increased feelings of anger, frustration and sadness.";
        _dialogueOne[5] = $"He continued to repress and push these feelings down hoping they would eventually stay down, never to reappear.";
        _dialogueOne[6] = $"Until one day, when it all became too much, and the World as he knew it had lost all it's color...";
        _dialogueOne[7] = $"Overwhelmed with his emotions, {_firstName} blacks out and faces a battle in his head against his demons";
    }

    // Finishes the remaining walk to the Therapists office - Grey
    void DialogueCutsceneTwo()
    {
        _dialogueTwo[0] = $"As {_firstName} wakes up, he realises it was at this point he knew he needed some help with his issues to be able to face his strongest antagonist....himself";
        _dialogueTwo[1] = $"* Jacob enters his Therapists office *";
    }

    // Leaves the Therapists office to walk home - Color
    void DialogueCutsceneThree()
    {
        _dialogueThree[0] = $"With a feeling of pure happiness and a new outlook on world, {_firstName} was on a path to a happier future, being able to breath properly for the first time in a long time";
    }

    // Switch through cutscene index to play appropriate cutscene
    // Called from Signal Emitter on Timeline at the start of the game
    public void PlayCutsceneRoutine()
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
                yield return _textDelayTime;        // Time between characters being displayed
            }

            _currentStringIndex++;
            yield return _clearTextBoxDelay;        // When the final text has finished, wait time before it is cleared.
            UIManager.Instance.ClearTextBox();
        }

        _currentCutsceneIndex++;
        _cutsceneRoutine = null;
    }
}
