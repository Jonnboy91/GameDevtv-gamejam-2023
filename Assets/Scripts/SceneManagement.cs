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

    private int _currentSceneIndex = 1;

    Coroutine _nextSceneCoroutine;


    private void Start()
    {
        DontDestroyOnLoad(this);    
    }

    // Only loads first scene
    public void StartGame()
    {
        FadeTransition.Instance.FadeIn();
        StartCoroutine(DelayFirstSceneLoad());
    }

    // Coroutine to allow fade transition to happen for first scene
    IEnumerator DelayFirstSceneLoad()
    {
        _currentSceneIndex++;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }

    // Must be called 2 secs before a scene loads
    public void NextScene()
    {
        Debug.Log("Test");
        StartCoroutine(NextSceneRoutine());
        Debug.Log("Test 01");
    }

    IEnumerator NextSceneRoutine()
    {
        Debug.Log("Test 03");
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Test 04");

        if (_currentSceneIndex < 5)
        {
            SceneManager.LoadScene(_currentSceneIndex + 1);
            _currentSceneIndex++;
        }
        else
        {
            SceneManager.LoadScene(0);
            _currentSceneIndex = 0;
        }

        Debug.Log("Lucky");
        _nextSceneCoroutine = null;
    }
}
