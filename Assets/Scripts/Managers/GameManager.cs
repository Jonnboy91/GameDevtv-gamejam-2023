using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _mainMenuButtons;


    // Only loads first scene
    public void StartGame()
    {
        StartCoroutine(DelayFirstSceneLoad());  
    }

    IEnumerator DelayFirstSceneLoad()
    {
        for (int i = 0; i < _mainMenuButtons.Length; i++)
        {
            _mainMenuButtons[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level 1");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
