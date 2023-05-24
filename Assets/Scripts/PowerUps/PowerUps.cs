using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    // ParentsProtectionPowerUp + FirstLovePowerUp, combo needs testing,
    // since IF the extraLife function is called exactly at the same time, not sure if there is a possibility of a bug

    private void Start() {
        // These needs to be setup so that it checks if the powerup has been chosen and if so then this can be activated.
        if(false){
            ImaginaryFriendPowerUp.instance.ActivatePowerup();
        }
        if(false){
            SugarRushPowerUp.instance.ActivatePowerup();
        }
        if(false){
            ParentsProtectionPowerUp.instance.ActivatePowerup();
        }
        if(false){
            GetOutOfTroublePowerUp.instance.ActivatePowerup();
        }
        if(false){
            TeenageAngstPowerUp.instance.ActivatePowerup();
        }
        if(false){
            FirstLovePowerUp.instance.ActivatePowerup();
        }
    }   
}
