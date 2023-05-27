using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieManager : MonoBehaviour
{
    public static DieManager instance;
    public float delaySeconds = 1f;

    [SerializeField] GameObject panel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ReloadLevelWithDelay()
    {
        StartCoroutine(showPanelWithDelay());
    }

    private IEnumerator showPanelWithDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        PauseGame();
        ShowPanel();
    }

    private void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void TryAgain(){
        Time.timeScale = 1;
        panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame(){
        Time.timeScale = 1;
        panel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void WinGame(){
        StartCoroutine(JumpToCutScene3());
    }

    private IEnumerator JumpToCutScene3()
    {
        yield return new WaitForSeconds(delaySeconds);
        // This should reload cutscene 3 // TODO: This should be done with the whole sceneManager, but for now it's here, didn't want to edit the scenemanager if you had edits there
        SceneManager.LoadScene("MainMenu");
    }

}
