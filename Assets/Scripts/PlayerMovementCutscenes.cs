using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementCutscenes : MonoBehaviour
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

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void updateSpeed()
    {
        moveSpeed += 2;
    }

    void Move()
    {
        if (Time.timeScale != 0)
        {
            Vector2 playerVelocity = moveInput * moveSpeed;
            myRigidBody.velocity = playerVelocity;
            if (playerVelocity.y != 0 && playerVelocity.x != 0)
            {
                _playerAnimations.ChangeState(new Vector2(0, playerVelocity.y));
            }
            else
            {
                _playerAnimations.ChangeState(playerVelocity);
            }
        }
    }
}
