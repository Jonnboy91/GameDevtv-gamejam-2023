using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinPowerUp : MonoBehaviour
{

    public static TwinPowerUp instance;

    private GameObject player;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DestroyScriptInstance()
    {
        // Done because sometimes when you die, it might have gotten inside of the line 44 and tries to rotate the ghost around the player and neither exist in the next scene.
        Destroy(this);
    }


    public void ActivatePowerup()
    {
        player.GetComponent<PlayerShooting>().DoubleTheBullets();
    }
}
