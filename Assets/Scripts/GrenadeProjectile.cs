using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D objCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objCollider = GetComponent<Collider2D>();
        rb.linearVelocityY = 5f;
        rb.linearVelocityX = 0f;
        objCollider.enabled = false; // temporarily disables upon awaken
        Invoke("EnableCollider", 1);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       Destroy(gameObject);
       if (collision.transform.CompareTag("Enemy"))
        {
            StatTracker.Instance.UpdateEnemyHealth(-1);
        }
    }
    private void EnableCollider()
    {
        objCollider.enabled = true;
    }
}
