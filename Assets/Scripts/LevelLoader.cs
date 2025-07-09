using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private SceneManager sceneManager;
    private void Awake()
    {

        StatTracker.OnGameOver += LoadEndScreen; // ties the end screen loading to the OnGameOver event in StatTracker
        StatTracker.OnLevelWin += LoadNextScreen; // ties the end screen loading to the OnGameOver event in StatTracker
    }

    public void LoadEndScreen()
    {
        Debug.Log("screen end called");
        SceneManager.LoadScene("GameOverScreen");
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("Start Screen"); // change this once you rename the actual scene
    }
    public void LoadNextScreen()
    {
        SceneManager.LoadScene("Win Screen");
    }
}
