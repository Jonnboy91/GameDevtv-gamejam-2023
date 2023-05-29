using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpPickup : MonoBehaviour
{
    GameObject player;
    [SerializeField] int xpAmount = 5;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            player.GetComponent<Experience>().IncreaseExperience(xpAmount);
            Destroy(gameObject);
        }
    }
}
