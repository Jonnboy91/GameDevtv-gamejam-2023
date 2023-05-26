using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
