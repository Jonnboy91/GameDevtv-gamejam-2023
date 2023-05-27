using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    #region Singleton
    private static FadeTransition _instance;
    public static FadeTransition Instance
    {
        get { return _instance; }   
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    private Animator _animator;



    private void Start()
    {
        _animator = GetComponent<Animator>();
        FadeIn();
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
