using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField] int neededExperience = 10;
    [SerializeField] Slider experienceSlider;
    private int currentExperience = 0;

    private void Awake() {
        experienceSlider.maxValue = neededExperience;
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
