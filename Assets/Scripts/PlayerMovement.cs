using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    PlayerAnimations _playerAnimations;


    float gravityScaleAtStart;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        gravityScaleAtStart = myRigidBody.gravityScale;

    }

    void FixedUpdate() 
    {
        Move();
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }


    void OnJump(InputValue value){
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }

        if(value.isPressed){
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Move(){
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        _playerAnimations.ChangeState(playerVelocity);
        myRigidBody.velocity = playerVelocity;
    }

}
