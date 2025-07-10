using System;
using UnityEngine;
using TMPro;

public class StatTracker : MonoBehaviour
{
    // THIS SCRIPT HANDLES ALL GLOBAL VARIABLES
    public static StatTracker Instance { get; private set; } // Singleton instance
    public static event Action OnGameOver; // Event triggered when player dies
    public static event Action OnLevelWin;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI grenadeText;

    [SerializeField] private int playerLives = 100;
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
