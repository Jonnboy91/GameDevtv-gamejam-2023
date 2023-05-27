using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        FadeTransition.Instance.FadeOut();
        SceneManagement.Instance.NextScene();
    }
}
