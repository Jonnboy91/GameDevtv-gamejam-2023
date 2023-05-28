using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetOutOfTroublePowerUp : MonoBehaviour
{
    public static GetOutOfTroublePowerUp instance;

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
        player.GetComponent<PlayerMovement>().updateSpeed();
        if(SceneManager.GetActiveScene().name == "Level 1"){
                player.GetComponent<EnemySpawner>().IncreaseSpawnRate();
        }
        
    }
}
