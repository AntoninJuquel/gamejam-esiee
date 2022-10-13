using ScreenNavigation.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private StackNavigator _navigator;
    private int _currentLevelIndex;

    public void Restart()
    {
        LoadLevel(_currentLevelIndex);
    }

    public void Next()
    {
        LoadLevel(_currentLevelIndex + 1);
    }

    public void LoadLevel(int i)
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(_currentLevelIndex);
        }

        _currentLevelIndex = i;
        SceneManager.LoadScene(i, LoadSceneMode.Additive);
        _navigator.Navigate("HUD");
    }

    public void GoToMenu()
    {
        _navigator.Navigate("Menu");
        SceneManager.LoadScene(0);
    }
}