using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTopDown : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    PlayerAnimations _playerAnimations;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        _playerAnimations = GetComponent<PlayerAnimations>();
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void Move(){
        Vector2 playerVelocity = moveInput * moveSpeed;
        myRigidBody.velocity = playerVelocity;
        _playerAnimations.ChangeState(playerVelocity);
    }

}
