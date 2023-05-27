using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "Imaginary"){
            Destroy(gameObject);
        }
    }

}
