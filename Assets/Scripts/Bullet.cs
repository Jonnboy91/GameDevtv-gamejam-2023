using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private bool doNotGiveUpActivated = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!doNotGiveUpActivated && other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "Imaginary"){
            Destroy(gameObject);
        } else if(other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "Imaginary"){
            doNotGiveUpActivated = false;
        }
    }

    public void DoNotGiveUp() {
        doNotGiveUpActivated = true;
    }

}
