using System.Collections;
using UnityEngine;

public class CutsceneEmojis : MonoBehaviour
{
    [SerializeField] private Sprite[] _emojiIcons;
    [SerializeField] private Sprite[] _emojiIconsSceneTwo;
    [SerializeField] private Sprite[] _emojiIconsSceneThree;
    private SpriteRenderer _spriteRenderer;

    WaitForSeconds _emojiDelay = new WaitForSeconds(1.5f);
    WaitForSeconds _hideEmoji = new WaitForSeconds(1.5f);

    private int currentSpriteIndex = 0;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("PlayEmojis", 2f);
    }

    // Signal call from Playable Director
    public void PlayEmojis()
    {
        StartCoroutine(ShowEmojisRoutine());    
    }

    // Ends when it reaches the last emoji
    IEnumerator ShowEmojisRoutine()
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
}
