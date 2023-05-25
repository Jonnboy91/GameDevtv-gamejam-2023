using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    private Animator _animator;



    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        _animator.SetTrigger("Fade Out");
        Debug.Log("Fading out");
    }

    public void FadeIn()
    {
        _animator.SetTrigger("Fade In");
        Debug.Log("Fading in");
    }
}
