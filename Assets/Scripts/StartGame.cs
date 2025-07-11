using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }
}