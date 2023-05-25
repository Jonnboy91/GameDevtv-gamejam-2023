using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEmojis : MonoBehaviour
{
    [SerializeField] private Sprite[] _emojiIcons;
    private SpriteRenderer _spriteRenderer;

    WaitForSeconds _emojiDelay = new WaitForSeconds(1.4f);
    WaitForSeconds _hideEmoji = new WaitForSeconds(1.5f);

    private int currentSpriteIndex = 0;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
