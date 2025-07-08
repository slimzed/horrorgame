using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private SceneManager sceneManager;
    private void Awake()
    {

        StatTracker.OnGameOver += LoadEndScreen; // ties the end screen loading to the OnGameOver event in StatTracker
    }

    private void LoadEndScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // change this once you rename the actual scene
    }
}
