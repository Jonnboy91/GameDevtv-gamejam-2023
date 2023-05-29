using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    #region Singleton
    private static SceneManagement _instance;
    public static SceneManagement Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    // Only loads first scene
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(DelayFirstSceneLoad());
    }

    // Coroutine to allow fade transition to happen for first scene
    IEnumerator DelayFirstSceneLoad()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }

    // Must be called 2 secs before a scene loads
    public void NextScene()
    {
        StartCoroutine(NextSceneRoutine());
    }

    IEnumerator NextSceneRoutine()
    {
        Debug.Log("Test 01");
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Test 02");
        if (SceneManager.GetActiveScene().buildIndex < 5)

        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
}
