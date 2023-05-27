using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] _mainMenuButtons;


    public void HideButtons()
    {
        for (int i = 0; i < _mainMenuButtons.Length; i++)
        {
            _mainMenuButtons[i].SetActive(false);
        }
    }
}
