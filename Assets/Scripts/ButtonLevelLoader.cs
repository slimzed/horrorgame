using UnityEngine;

public class ButtonLevelLoader : MonoBehaviour
{
    GameObject ScriptCalls;
    LevelLoader levelLoader;
    void Start()
    {
        ScriptCalls = GameObject.Find("ScriptCalls");
        levelLoader = ScriptCalls.GetComponent<LevelLoader>();
    }

    public void OnClick()
    {
        levelLoader.LoadNextLevel();
    }
}
