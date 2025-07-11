using UnityEngine;

public class LifeIcon : MonoBehaviour
{
    [SerializeField] private GameObject lifeIcon;
    private int lives;
    private void Start()
    {
        lives = StatTracker.Instance.GetHealth(); 
        for (int i=0; i<lives; i++)
        {
            GameObject icon = Instantiate(lifeIcon, new Vector3(transform.position.x + 15*i, transform.position.y, transform.position.z), Quaternion.identity);
            lifeIcon.transform.SetParent(icon.transform, false);
        }
    }
}
