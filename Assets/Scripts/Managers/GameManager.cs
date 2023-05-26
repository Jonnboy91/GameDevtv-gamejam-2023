using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator DelayFirstSceneLoad()
    {
        for (int i = 0; i < _mainMenuButtons.Length; i++)
        {
            _mainMenuButtons[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Cutscene 1");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
