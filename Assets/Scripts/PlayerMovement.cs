using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    Animator animator;
    


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    public void updateSpeed() {
        moveSpeed *= 1.2f;
    }

    public float GetSpeed() {
        return moveSpeed;
    }

    void Move(){
        if(Time.timeScale != 0){
            Vector2 playerVelocity = moveInput * moveSpeed;
            myRigidBody.velocity = playerVelocity;
            gameObject.GetComponent<SpriteRenderer>().flipX = playerVelocity.x < 0;
            if(playerVelocity.x != 0 || playerVelocity.y != 0){
                animator.SetBool("isWalking", true);
            }else{
                animator.SetBool("isWalking", false);
            }
        }

    }

}
