using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeState(Vector2 value)
    {
            _animator.SetFloat("X Float", value.x);
            _animator.SetFloat("Y Float", value.y);
    }
}
