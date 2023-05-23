using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    void Awake() {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update() {
        if(player != null){
            SetAgentPosition();
            RotateEnemyTowardsPlayer();
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(player.transform.position);
    }

    void RotateEnemyTowardsPlayer(){
        Vector3 look = transform.InverseTransformPoint(player.transform.position);    
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg -90;

        transform.Rotate(0,0, angle);
    }
}
