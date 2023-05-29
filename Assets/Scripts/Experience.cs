using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class Experience : MonoBehaviour
{
    [SerializeField] int neededExperience = 20;
    private int addExp;
    private int maxExperience;
    private int currentLevel = 0;
    [SerializeField] Slider experienceSlider;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] GameObject powerUpPanel;
    private int currentExperience = 0;

    Health health;


    private bool lastPanelOpen = false; // This is used, since otherwise the Update is being called all the time and it's pausing the game just before scenemanager loads the final boss level, and it starts frozen.

    private void Awake() {
        addExp = neededExperience;
        maxExperience = (neededExperience * (Enum.GetNames(typeof(PowerUpEnums)).Length - 3)); // this can be only timed by a number that is <= amount of different powerUps - 3 (since last panel has to have 3 powerUps to choose from)
        experienceText.text = $"{currentLevel}/{Enum.GetNames(typeof(PowerUpEnums)).Length - 3}";
        experienceSlider.maxValue = neededExperience;
        health = GetComponent<Health>();
    }

    private void Update() {
        if(currentExperience == maxExperience && !lastPanelOpen){
            lastPanelOpen = true;
            PauseGame();
            OpenPowerUpUI(true);
        } else if(currentExperience >= neededExperience && currentExperience < maxExperience && !PowerUps.instance.isLookingAtPowerUps){
            PauseGame();
            OpenPowerUpUI(false);
            UpdateNeededExperience();
        }
    }

    private void OpenPowerUpUI(bool isLastPanel)
    {
        currentLevel++;
        updateLevelText();
        if(isLastPanel){
            PowerUps.instance.SetPowerUpToButtons(true);
        }else{
            PowerUps.instance.SetPowerUpToButtons(false);
        }
        powerUpPanel.SetActive(true);
    }

    private void updateLevelText(){
        experienceText.text = $"{currentLevel}/{Enum.GetNames(typeof(PowerUpEnums)).Length - 3}";
    }

    private void UpdateNeededExperience()
    {
        neededExperience += addExp;
        experienceSlider.maxValue = neededExperience;
        health.addHealth();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void IncreaseExperience(int amount)
    {
        currentExperience += amount;
        UpdateExperienceUI();
    }

    private void UpdateExperienceUI()
    {
        experienceSlider.value = currentExperience;
    }

}
