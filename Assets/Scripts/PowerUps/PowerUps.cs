using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

// If more enums should be used, this should be in enums script.
public enum PowerUpEnums {
        Imaginary,
        Sugar,
        Parents,
        Trouble,
        Angst,
        Love,
        Pay,
        Independence,
        Popeye,
        Twin,
        DoNotGiveUp
    }

public class PowerUps : MonoBehaviour
{
    
     public static PowerUps instance;

    [SerializeField] GameObject powerUpPanel;
    [SerializeField] List<Button> powerUpButtons;
    [SerializeField] Sprite imaginaryImage;
    [SerializeField] Sprite sugarImage;
    [SerializeField] Sprite parentsImage;
    [SerializeField] Sprite troubleImage;
    [SerializeField] Sprite angstImage;
    [SerializeField] Sprite loveImage;
    [SerializeField] Sprite payImage;
    [SerializeField] Sprite independenceImage;
    [SerializeField] Sprite popeyeImage;
    [SerializeField] Sprite twinImage;
    [SerializeField] Sprite doNotGiveUpImage;

    public bool isLookingAtPowerUps = false;



    List<PowerUpEnums> possiblePowerUps = new List<PowerUpEnums> { PowerUpEnums.Imaginary, PowerUpEnums.Sugar, PowerUpEnums.Parents, PowerUpEnums.Trouble, PowerUpEnums.Angst, PowerUpEnums.Love, PowerUpEnums.Pay, PowerUpEnums.Independence, PowerUpEnums.Popeye, PowerUpEnums.Twin, PowerUpEnums.DoNotGiveUp};

    // TODO: Add PlayerPrefs.DeleteAll() to start game button! IMPORTANT!

    // Creates an instance of PowerUps and checks if any of the powerUps are active (Just for bosslevel) and if so, then activates them.
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    
    private void Start() {
        
        if(PlayerPrefs.GetInt("Imaginary") == 1){
            ImaginaryFriendPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Sugar") == 1){
            SugarRushPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Parents") == 1){
            ParentsProtectionPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Trouble") == 1){
            GetOutOfTroublePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Angst") == 1){
            TeenageAngstPowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Love") == 1){
            FirstLovePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Pay") == 1){
            PayRisePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Independence") == 1){
            IndepencePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Popeye") == 1){
            PopeyePowerUp.instance.ActivatePowerup();
        }
        if(PlayerPrefs.GetInt("Twin") == 1){
            TwinPowerUp.instance.ActivatePowerup();
        }
         if(PlayerPrefs.GetInt("DoNotGiveUp") == 1){
            DoNotGiveUpPowerUp.instance.ActivatePowerup();
        }
    }

    public void SetPowerButtonText(Button PowerUpButton, string powerUpName, string powerUpDescription){
        TextMeshProUGUI nameText = PowerUpButton.transform.Find("PowerUpName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = PowerUpButton.transform.Find("PowerUpDescription").GetComponent<TextMeshProUGUI>();

        nameText.text = powerUpName;
        descriptionText.text = powerUpDescription;
    }

    public void SetPowerButtonImage(Button PowerUpButton, Sprite image){
        Image buttonImage = PowerUpButton.transform.Find("PowerUpImage").GetComponent<Image>();

        buttonImage.sprite = image;
    }

    public void SetPowerUpToButtons(bool isLastPanel){
        isLookingAtPowerUps = true;
        PowerUpEnums randomPowerUp1 = possiblePowerUps[Random.Range(0, possiblePowerUps.Count)];
        List<PowerUpEnums> remainingPowerUps = new List<PowerUpEnums>(possiblePowerUps);
        remainingPowerUps.Remove(randomPowerUp1);

        int randomIndex2 = Random.Range(0, remainingPowerUps.Count);
        int randomIndex3 = Random.Range(0, remainingPowerUps.Count - 1);

        if (randomIndex3 >= randomIndex2)
            randomIndex3++;

        PowerUpEnums randomPowerUp2 = remainingPowerUps[randomIndex2];
        PowerUpEnums randomPowerUp3 = remainingPowerUps[randomIndex3];

        List<PowerUpEnums> powerUps = new List<PowerUpEnums> { randomPowerUp1, randomPowerUp2, randomPowerUp3 };
        
        for (int i = 0; i < powerUpButtons.Count; i++)
        {
            AddPowerToButton(powerUpButtons[i], powerUps[i], isLastPanel);
        }
    }

    private void AddPowerToButton(Button button, PowerUpEnums powerUp, bool isLastPanel)
    {
        button.onClick.RemoveAllListeners();
        switch (powerUp)
        {
            case PowerUpEnums.Imaginary:
                SetPowerButtonText(button, "Imaginary friend", "Summon your childhood supporter");
                SetPowerButtonImage(button, imaginaryImage);
                button.onClick.AddListener(delegate {ChooseImaginaryFriend(isLastPanel);});
                break;
            case PowerUpEnums.Sugar:
                SetPowerButtonText(button, "Sugar rush", "Increase speed by 20%");
                SetPowerButtonImage(button, sugarImage);
                button.onClick.AddListener(delegate {ChooseSugarRushPowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Parents:
                SetPowerButtonText(button, "Parents protection", "Increase health by 20%");
                SetPowerButtonImage(button, parentsImage);
                button.onClick.AddListener(delegate {ChooseParentsProtectionPowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Trouble:
                SetPowerButtonText(button, "Trouble", "Increase speed by 20%");
                SetPowerButtonImage(button, troubleImage);
                button.onClick.AddListener(delegate {ChooseGetOutOfTroublePowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Angst:
                SetPowerButtonText(button, "Teenage angst", "Increase bullet damage by 10%");
                SetPowerButtonImage(button, angstImage);
                button.onClick.AddListener(delegate {ChooseTeenageAngstPowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Love:
                SetPowerButtonText(button, "First love", "Increase health by 20%");
                SetPowerButtonImage(button, loveImage);
                button.onClick.AddListener(delegate {ChooseFirstLovePowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Pay:
                SetPowerButtonText(button, "Pay Rise", "Increase bullet lifespan by 10%");
                SetPowerButtonImage(button, payImage);
                button.onClick.AddListener(delegate {ChoosePayRisePowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Independence:
                SetPowerButtonText(button, "Independence", "Increase speed by 20%");
                SetPowerButtonImage(button, independenceImage);
                button.onClick.AddListener(delegate {ChooseIndepencePowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Popeye:
                SetPowerButtonText(button, "Popeye", "Increase bullet damage by 10%");
                SetPowerButtonImage(button, popeyeImage);
                button.onClick.AddListener(delegate {ChoosePopeyePowerUp(isLastPanel);});
                break;
            case PowerUpEnums.Twin:
                SetPowerButtonText(button, "Twin", "Double the trouble");
                SetPowerButtonImage(button, twinImage);
                button.onClick.AddListener(delegate {ChooseTwinPowerUp(isLastPanel);});
                break;
            case PowerUpEnums.DoNotGiveUp:
                SetPowerButtonText(button, "DoNotGiveUp", "Bullet doesn't destroy itself on the first hit");
                SetPowerButtonImage(button, doNotGiveUpImage);
                button.onClick.AddListener(delegate {ChooseDoNotGiveUpPowerUp(isLastPanel);});
                break;
            default:
                Debug.Log("SHOULD GET HERE AT ALL IN ANY POINT! CHECK IF HERE AT ANY POINT! POWERUPS.CS SCRIPT");
                break;
        }
    }

    public void ChooseImaginaryFriend(bool isLastPanel){
        PlayerPrefs.SetInt("Imaginary", 1);
        ImaginaryFriendPowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Imaginary);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }

    public void ChooseSugarRushPowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Sugar", 1);
        SugarRushPowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Sugar);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChooseParentsProtectionPowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Parents", 1);
        ParentsProtectionPowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Parents);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChooseGetOutOfTroublePowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Trouble", 1);
        GetOutOfTroublePowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Trouble);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChooseTeenageAngstPowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Angst", 1);
        TeenageAngstPowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Angst);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChooseFirstLovePowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Love", 1);
        FirstLovePowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Love);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChoosePayRisePowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Pay", 1);
        PayRisePowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Pay);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChooseIndepencePowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Independence", 1);
        IndepencePowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Independence);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChoosePopeyePowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Popeye", 1);
        PopeyePowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Popeye);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }

    public void ChooseTwinPowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("Twin", 1);
        TwinPowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.Twin);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }
    public void ChooseDoNotGiveUpPowerUp(bool isLastPanel){
        PlayerPrefs.SetInt("DoNotGiveUp", 1);
        DoNotGiveUpPowerUp.instance.ActivatePowerup();
        possiblePowerUps.Remove(PowerUpEnums.DoNotGiveUp);
        if(isLastPanel){
            JumpToCutScene();
        }else{
            ContinueGame();
        }
    }

    private void ContinueGame(){
        powerUpPanel.SetActive(false);
        isLookingAtPowerUps = false;
        Time.timeScale = 1;
    }

    private void JumpToCutScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Cutscene 2");
    }
}
