using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    AudioSource _audioSource;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();  
    }

    public void PlayShootSFX()
    {
        _audioSource.Play();
    }
}
