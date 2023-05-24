using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLovePowerUp : MonoBehaviour
{
   /*  Might need to set it up in one PowerUp script that's on the player,
   since this way it's cleaner and only one instance is created,
   BUT you need to have this "manager" script in all of the scenes,
   where it COULD be used by the player and it will create the instance,
   not sure how taxing that is for the system. 
   AND this would have to be for all the 9 different powerups 
   (obviously on childhood there would be none, adolesence there 
   would be 3 adulthood 6 and final boss would have the 9) */

    public static FirstLovePowerUp instance;

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
        player.GetComponent<Health>().extraLife();
    }
}
