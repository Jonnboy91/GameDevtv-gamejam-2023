using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieManager : MonoBehaviour
{
    public static DieManager instance;
    public float delaySeconds = 2f; // Adjust this value to set the desired delay

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ReloadLevelWithDelay()
    {
        StartCoroutine(ReloadLevelCoroutine());
    }

    private IEnumerator ReloadLevelCoroutine()
    {
        yield return new WaitForSeconds(delaySeconds);
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
