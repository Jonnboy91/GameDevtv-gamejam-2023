using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEmojis : MonoBehaviour
{
    [SerializeField] private Sprite[] _emojiIcons;
    [SerializeField] private Sprite[] _emojiIconsSceneTwo;
    [SerializeField] private Sprite[] _emojiIconsSceneThree;
    private SpriteRenderer _spriteRenderer;

    WaitForSeconds _emojiDelay = new WaitForSeconds(1.45f);
    WaitForSeconds _hideEmoji = new WaitForSeconds(1.5f);

    private int currentSpriteIndex = 0;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("PlayEmojis", 2f);
    }

    public void PlayEmojis()
    {
        int result = SceneManager.GetActiveScene().buildIndex;

        switch (result)
        {
            case 0:
            case 1:
                StartCoroutine(ShowEmojisCutsceneOneRoutine());    
                break;
            case 2:
            case 3:
                StartCoroutine(ShowEmojisCutsceneTwoRoutine());    
                break;
            case 4:
            case 5:
                StartCoroutine(ShowEmojisCutsceneThreeRoutine());    
                break;
            default:
                Debug.Log("Incorect build index number");
                break;
        }
    }

    IEnumerator ShowEmojisCutsceneOneRoutine()
    {
        while (currentSpriteIndex < _emojiIcons.Length)
        {
            yield return _hideEmoji;    // Wait 2 seconds
            _spriteRenderer.sprite = _emojiIcons[currentSpriteIndex];
            yield return _emojiDelay;   // Wait 2 seconds
            _spriteRenderer.sprite = null;
            currentSpriteIndex++;
        }
    }

    IEnumerator ShowEmojisCutsceneTwoRoutine()
    {
        while (currentSpriteIndex < _emojiIconsSceneTwo.Length)
        {
            yield return _hideEmoji;    // Wait 2 seconds
            _spriteRenderer.sprite = _emojiIconsSceneTwo[currentSpriteIndex];
            yield return _emojiDelay;   // Wait 2 seconds
            _spriteRenderer.sprite = null;
            currentSpriteIndex++;
        }
    }

    IEnumerator ShowEmojisCutsceneThreeRoutine()
    {
        while (currentSpriteIndex < _emojiIconsSceneThree.Length)
        {
            yield return _hideEmoji;    // Wait 2 seconds
            _spriteRenderer.sprite = _emojiIconsSceneThree[currentSpriteIndex];
            yield return _emojiDelay;   // Wait 2 seconds
            _spriteRenderer.sprite = null;
            currentSpriteIndex++;
        }
    }
}
