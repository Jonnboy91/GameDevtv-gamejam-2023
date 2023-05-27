using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class PowerUps : MonoBehaviour
{

    [SerializeField] List<GameObject> panels;

    // ParentsProtectionPowerUp + FirstLovePowerUp, combo needs testing,
    // since IF the extraLife function is called exactly at the same time, not sure if there is a possibility of a bug

    // TODO: Add PlayerPrefs.DeleteAll() to start game button! IMPORTANT!
    private void Awake() {
        if(PlayerPrefs.GetInt("Imaginary") == 1){ // Childhood 1
            ImaginaryFriendPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Sugar") == 1){ // Childhood 2
            SugarRushPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Parents") == 1){ // Childhood 3
            ParentsProtectionPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Trouble") == 1){ // Adolescence  1
            GetOutOfTroublePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Angst") == 1){ // Adolescence  2
            TeenageAngstPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Love") == 1){ // Adolescence  3
            FirstLovePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Pay") == 1){ // Adult 1
            PayRisePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Independence") == 1){ // Adult 2
            IndepencePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Popeye") == 1){ // Adult 3
            PopeyePowerUp.instance.ActivatePowerup();
        }
        
    }

    public void ChooseImaginaryFriend(){
        PlayerPrefs.SetInt("Imaginary", 1);
        ImaginaryFriendPowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChooseSugarRushPowerUp(){
        PlayerPrefs.SetInt("Sugar", 1);
        SugarRushPowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChooseParentsProtectionPowerUp(){
        PlayerPrefs.SetInt("Parents", 1);
        ParentsProtectionPowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChooseGetOutOfTroublePowerUp(){
        PlayerPrefs.SetInt("Trouble", 1);
        GetOutOfTroublePowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChooseTeenageAngstPowerUp(){
        PlayerPrefs.SetInt("Angst", 1);
        TeenageAngstPowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChooseFirstLovePowerUp(){
        PlayerPrefs.SetInt("Love", 1);
        FirstLovePowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChoosePayRisePowerUp(){
        PlayerPrefs.SetInt("Pay", 1);
        PayRisePowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChooseIndepencePowerUp(){
        PlayerPrefs.SetInt("Independence", 1);
        IndepencePowerUp.instance.ActivatePowerup();
        ContinueGame();
    }
    public void ChoosePopeyePowerUp(){
        PlayerPrefs.SetInt("Popeye", 1);
        PopeyePowerUp.instance.ActivatePowerup();
        ContinueGame();
    }

    private void ContinueGame(){
        panels.First().SetActive(false);
        panels.RemoveAt(0);
        Time.timeScale = 1;
    }

    public void LastPayRisePowerUpJumpToCutscene(){
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Pay", 1);
        PayRisePowerUp.instance.ActivatePowerup();
        SceneManager.LoadScene("Boss");
    }

    public void LastIndepencePowerUpJumpToCutscene(){
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Independence", 1);
        IndepencePowerUp.instance.ActivatePowerup();
        SceneManager.LoadScene("Boss");
    }
    public void LastPopeyePowerUpJumpToCutscene(){
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Popeye", 1);
        PopeyePowerUp.instance.ActivatePowerup();
        SceneManager.LoadScene("Boss");
    }
}
