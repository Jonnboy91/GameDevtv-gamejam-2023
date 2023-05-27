using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float enemyDamage = 10;

    public float GetDamage(){
        return enemyDamage;
    }

    public void setEnemyDamage(float damage){
        enemyDamage = damage;
    }
}
