using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float SpawnProtection = 1f;
    private Collider2D objCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objCollider = GetComponent<Collider2D>();
        rb.linearVelocityY = -3f;
        rb.linearVelocityX = Random.Range(-1f, 1f);
        objCollider.enabled = false; // temporarily disables upon awaken
        Invoke("EnableCollider", SpawnProtection); //xz waits 1 second before reenabling collider

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject != null && collision.CompareTag("Respawn"))
        {
            Destroy(gameObject);
            if (StatTracker.Instance != null)
            {
                StatTracker.Instance.SubtractLives();
            }
            else
            {
                Debug.LogWarning("stat-tracker script not found");
            }

        }     
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            // play explosion animatino here 
            StatTracker.Instance.SubtractLives();
        }
    }
    private void EnableCollider()
    {
        objCollider.enabled = true;
    }

}
