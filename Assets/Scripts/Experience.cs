using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField] int neededExperience = 10;
    [SerializeField] int maxExperience = 30;
    [SerializeField] Slider experienceSlider;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject lastPanel;
    private int currentExperience = 0;

    private void Awake() {
        experienceSlider.maxValue = neededExperience;
    }

    private void Update() {
        if(currentExperience < maxExperience){
            PauseGame();
            OpenLastPowerUpUI();
        } else if(currentExperience == neededExperience){
            PauseGame();
            OpenPowerUpUI();
            UpdateNeededExperience();
        }
    }

    private void OpenPowerUpUI()
    {
        panel.SetActive(true);
    }
    private void OpenLastPowerUpUI()
    {
        lastPanel.SetActive(true);
    }

    private void UpdateNeededExperience()
    {
        neededExperience += 10;
        experienceSlider.maxValue = neededExperience;
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
