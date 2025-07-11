using UnityEngine;

public class ButtonBehaviorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClickButton()
    {
        transform.parent.gameObject.SetActive(false);
    }
    public void OnClickLives()
    {
        StatTracker.Instance.UpdatePlayerLives(3);
    }
    public void OnClickGrenades()
    {
        StatTracker.Instance.UpdateGrenadeCount(7);
    }
}
