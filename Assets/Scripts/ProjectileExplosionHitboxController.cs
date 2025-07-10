using UnityEngine;

public class ProjectileExplosionHitboxController : MonoBehaviour
{
    [SerializeField] private float ProjectileExplosionHitboxTime = 0.25f;

    private void OnEnable()
    {
        Invoke("DeleteObject", ProjectileExplosionHitboxTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatTracker.Instance.SubtractLives();
        }
    }

    private void DeleteObject()
    {
        Destroy(gameObject);
    }
}
