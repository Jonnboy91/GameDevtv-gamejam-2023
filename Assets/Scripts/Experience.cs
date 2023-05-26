using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Experience : MonoBehaviour
{
    [SerializeField] int neededExperience = 1;
    [SerializeField] int maxExperience = 4;
    [SerializeField] Slider experienceSlider;
    [SerializeField] List<GameObject> panels;
    [SerializeField] GameObject lastPanel;
    private int currentExperience = 0;

    private bool lastPanelOpen = false; // This is used, since otherwise the Update is being called all the time and it's pausing the game just before scenemanager loads the final boss level, and it starts frozen.

    private void Awake() {
        experienceSlider.maxValue = neededExperience;
    }

    private void Update() {
        if(currentExperience == maxExperience && !lastPanelOpen){
            lastPanelOpen = true;
            PauseGame();
            OpenLastPowerUpUI();
        } else if(currentExperience == neededExperience && currentExperience < maxExperience){
            PauseGame();
            OpenPowerUpUI();
            UpdateNeededExperience();
        }
    }

    private void OpenPowerUpUI()
    {
        panels.First().SetActive(true);
        panels.RemoveAt(0);
    }
    private void OpenLastPowerUpUI()
    {
        lastPanel.SetActive(true);
    }

    private void UpdateNeededExperience()
    {
        neededExperience += 1;
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
