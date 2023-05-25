using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    [SerializeField] GameObject panel;

    // ParentsProtectionPowerUp + FirstLovePowerUp, combo needs testing,
    // since IF the extraLife function is called exactly at the same time, not sure if there is a possibility of a bug

    private void Start() {
        // These needs to be setup so that it checks if the powerup has been chosen and if so then this can be activated.
        if(false){ // Childhood 1
            ImaginaryFriendPowerUp.instance.ActivatePowerup();
        }
        if(false){ // Childhood 2
            SugarRushPowerUp.instance.ActivatePowerup();
        }
        if(false){ // Childhood 3
            ParentsProtectionPowerUp.instance.ActivatePowerup();
        }
        if(false){ // Adolescence  1
            GetOutOfTroublePowerUp.instance.ActivatePowerup();
        }
        if(false){ // Adolescence  2
            TeenageAngstPowerUp.instance.ActivatePowerup();
        }
        if(false){ // Adolescence  3
            FirstLovePowerUp.instance.ActivatePowerup();
        }
        if(false){ // Adult 1
            PayRisePowerUp.instance.ActivatePowerup();
        }
        if(false){ // Adult 2
            IndepencePowerUp.instance.ActivatePowerup();
        }
        if(false){ // Adult 3
            PopeyePowerUp.instance.ActivatePowerup();
        }
        
    }   

    public void ContinueGame(){
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
