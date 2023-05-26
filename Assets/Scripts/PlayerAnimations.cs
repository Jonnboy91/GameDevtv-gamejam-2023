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
        if(value.x != 0 || value.y != 0){
            _animator.SetBool("isMoving", true);
            _animator.SetFloat("X Float", value.x);
            _animator.SetFloat("Y Float", value.y);
        }else{
            _animator.SetBool("isMoving", false);
            _animator.SetFloat("X Float", value.x);
            _animator.SetFloat("Y Float", value.y);
        }

    }
}
