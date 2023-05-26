using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private int _currentSceneIndex;

    Scene scene;

    [SerializeField] private GameObject[] _mainMenuButtons;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }

    // Only loads first scene
    public void StartGame()
    {
        StartCoroutine(DelayFirstSceneLoad());
    }

    // Coroutine to allow fade transition to happen for first scene
    IEnumerator DelayFirstSceneLoad()
    {
        for (int i = 0; i < _mainMenuButtons.Length; i++)
        {
            _mainMenuButtons[i].SetActive(false);
        }

        _currentSceneIndex++;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);

    }

    // Must be called 2 secs before a scene loads
    public void NextScene()
    {
        if(_currentSceneIndex < 5)
        {
            SceneManager.LoadScene(_currentSceneIndex + 1);
            _currentSceneIndex++;
        }
        else
        {
            SceneManager.LoadScene(0);
            _currentSceneIndex = 0;
        }
    }
}
