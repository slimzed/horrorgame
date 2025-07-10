using System;
using UnityEngine;
using TMPro;

public class StatTracker : MonoBehaviour
{
    public static StatTracker Instance { get; private set; } // Singleton instance
    public static event Action OnGameOver; // Event triggered when player dies
    public static event Action OnLevelWin;
    [SerializeField] private TextMeshProUGUI livesText;

    [SerializeField] private int playerLives = 5;
    [SerializeField] private int enemyHealth = 3;
    private int Remaining = 0;


    private void Awake()
    {
        Debug.Log(enemyHealth);
        Instance = this;
        UpdateUI();
        DontDestroyOnLoad(StatTracker.Instance.gameObject);
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

    public int GetLives()
    {
        return playerLives;
    }
    public int GetHealth()
    {
        return enemyHealth;
    }

    public void SetLives(int lives)
    {
        playerLives = lives;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (livesText != null && playerLives > 0)
        {
            livesText.text = "Player Lives: " + playerLives;
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
