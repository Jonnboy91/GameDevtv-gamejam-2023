using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Boss" && other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "EnemyImaginary" && other.gameObject.tag != "pickUp"){
            Destroy(gameObject);
        }
    }         
}
