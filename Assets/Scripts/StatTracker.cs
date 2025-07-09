using System;
using UnityEngine;
using TMPro;

public class StatTracker : MonoBehaviour
{
    public static StatTracker Instance { get; private set; } // creates a singleton instance so that I can access this within other scripts
    public static event Action OnGameOver; // creates a public action trigger that activates when the player is dead


    [SerializeField] private TextMeshProUGUI livesText;




    private int playerLives = 100;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void SubtractLives()
    {
        playerLives--;
        UpdateUI();
        if (playerLives <= 0)
        {
            OnGameOver.Invoke(); // invokes a global event that the game has ended
            Invoke("UpdateUI", 2f); // CHANGE THIS LATER SO THAT SOMETHING HAPPENS
        }
    }

    public int GetLives()
    {
        return playerLives;
    }
    public void SetLives(int lives)
    {
        playerLives = lives;
    }
    private void UpdateUI()
    {
        livesText.text = "Player Lives: " + playerLives;
        // once sprites are created, iterate over the number of lives and create independent life icons or whatever
    }



    public void SubtractHealth(int health, GameObject obj)
    {
        health--;
        if (health == 0)
        {
            Destroy(obj);
        }
    }
}
