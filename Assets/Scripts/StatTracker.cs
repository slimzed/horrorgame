using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net;
using System.Linq.Expressions;

public class StatTracker : MonoBehaviour
{
    // THIS SCRIPT HANDLES ALL GLOBAL VARIABLES
    public static StatTracker Instance { get; private set; } // Singleton instance
    public static event Action OnGameOver; // Event triggered when player dies
    public static event Action OnLevelWin;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI grenadeText;

    [SerializeField] private int playerLives = 5;
    [SerializeField] private int enemyHealth = 3;
    [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private int GrenadeCount = 7;
    private int Remaining = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Second instance created");
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        UpdateUI();

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoad;
    }


    private void HandleSceneLoad(Scene scene, LoadSceneMode mode) // searches for the text components every time the scene is reloaded, otherwise the StatTracker cannot directly access components
    {
        if (scene.name == "SampleScene") // first checks if we are in the sample scene, otherwise there is no point in grabbing the text elements
        {
            if (!livesText)
            {
                livesText = GameObject.FindWithTag("PlayerLivesText").GetComponent<TextMeshProUGUI>(); // i gave it the tag PlayerLivesText
            }
            if (!grenadeText)
            {
                grenadeText = GameObject.FindWithTag("GrenadeText").GetComponent<TextMeshProUGUI>(); // same here
            }
            UpdateUI();
        }
    }
    public int GetLives()
    {
        return playerLives;
    }
    
    public void SetLives(int lives)
    {
        playerLives = lives;
        UpdateUI();
    }
    public void UpdatePlayerLives(int AddedLives)
    {
        playerLives += AddedLives;
    }

    public void SubtractLives()
    {
        playerLives--;
        UpdateUI();

        if (playerLives <= 0)
        {
            OnGameOver?.Invoke(); // Safely trigger Game Over event
            enemyHealth++;
        }
    }
    public int GetHealth()
    {
        return enemyHealth;
    }
    public int GetGrenadeCounter()
    {
        return GrenadeCount;
    }
    public void SubtractGrenade()
    {
        GrenadeCount--;
        UpdateUI();
    }


    public void UpdateEnemyHealth(int AddedHealth)
    {
        enemyHealth+=AddedHealth;
    }

    public float GetMoveSpeed()
    {
        return playerMoveSpeed;
    }
    public void UpdateMoveSpeed(int speed)
    {
        playerMoveSpeed += speed;
    }
    public void UpdateGrenadeCount(int grenadeCount)
    {
        Debug.Log(GrenadeCount);
        GrenadeCount = grenadeCount;
        Debug.Log(GrenadeCount);
    }

    public void AddRemainingEnemies()
    {
        Remaining++;
    }
    public void SubtractRemainingEnemies()
    {
        Remaining--;
        if (Remaining <= 0)
        {
            OnLevelWin?.Invoke();
        }
    }


    private void UpdateUI()
    {
        if (livesText != null && playerLives > 0)
        {
            livesText.text = "Player Lives: " + playerLives;
            grenadeText.text = "Grenades: " + GrenadeCount;
        }
    }

    // Optional: only use this if you're tracking enemy or player health separately
    public void SubtractHealth(int health, GameObject obj)
    {
        health--;
        if (health <= 0)
        {
            Destroy(obj);
            OnGameOver?.Invoke(); 
        }
    }
}
