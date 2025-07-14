using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // this functino handles ALL GLOBAL LEVEL LOADING
    private SceneManager sceneManager;
    [SerializeField] private int LevelSceneIndex;
    [SerializeField] private AudioClip sfx;



    private AudioSource[] audioSources;
    private AudioSource source;
    private void Awake()
    {

        audioSources = GetComponents<AudioSource>();
        if (audioSources != null)
        {
            source = audioSources[0]; 
        }
        

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
        
        
        StatTracker.Instance.SetLives(5);
        StatTracker.Instance.SetGrenades(7);
        StatTracker.Instance.SetEnemyHealth(3);
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
        source.clip = sfx;
        source.Play();
        LevelSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(LevelSceneIndex + 1);
        StatTracker.Instance.UpdateEnemyHealth(1); // adds one health to the enemies
    }
}
