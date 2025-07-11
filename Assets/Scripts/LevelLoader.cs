using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // this functino handles ALL GLOBAL LEVEL LOADING
    private SceneManager sceneManager;
    [SerializeField] private int LevelSceneIndex;
    private void Awake()
    {

        StatTracker.OnGameOver += LoadEndScreen; // ties the end screen loading to the OnGameOver event in StatTracker
        StatTracker.OnLevelWin += LoadWinScreen; // ties the end screen loading to the OnGameOver event in StatTracker
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
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScreen");
    }
    public void LoadWinScreen()
    {
        SceneManager.LoadScene("Win Screen");
        LevelSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(LevelSceneIndex);
    }
    public void LoadNextLevel()
    {
        Debug.Log(LevelSceneIndex);
        SceneManager.LoadScene(LevelSceneIndex + 1);
        StatTracker.Instance.UpdateEnemyHealth(1); // adds one health to the enemies
    }
}
