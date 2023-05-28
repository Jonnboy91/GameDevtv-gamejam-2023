using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if(player != null){
            SetAgentPosition();
            FlipEnemy();
        }
    }

    void SetAgentPosition()
    {
        if(agent.isActiveAndEnabled){
            agent.SetDestination(player.position);
        }
    }

    void FlipEnemy(){
        spriteRenderer.flipX = player.position.x < gameObject.transform.position.x;
    }
}
