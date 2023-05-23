using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Topdown level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
