using System.Collections;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] GameObject ExplosionHitbox; 
    
    private Rigidbody2D rb;
    private float SpawnProtection = 1f;
    private Collider2D objCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objCollider = GetComponent<Collider2D>();
        rb.linearVelocityY = -3f;
        float lY = Random.Range(-4f, 4f);
        if (lY < 0) // hardcoded clamp
        {
            Mathf.Clamp(lY, -4f, 0.5f);
        } else
        {
            Mathf.Clamp(lY, 0.5f, 4f);
        }
        rb.linearVelocityX = lY;
        objCollider.enabled = false; // temporarily disables upon awaken
        Invoke("EnableCollider", SpawnProtection); // xz waits 1 second before reenabling collider
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject != null && collision.CompareTag("Respawn"))
        {
            if (StatTracker.Instance != null)
            {
                GameObject hitbox = Instantiate(ExplosionHitbox, gameObject.transform.position, Quaternion.identity);
                hitbox.transform.SetParent(gameObject.transform.parent);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("stat-tracker script not found");
            }

        }     
        else if (collision.CompareTag("Player"))
        {
            PlayerVisuals playerVisuals = collision.gameObject.GetComponent<PlayerVisuals>();
            playerVisuals.FlashRedTemporarily(0.25f, gameObject);
            StatTracker.Instance.SubtractLives();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Respawn"))
        {
            Destroy(gameObject); // destroys the object when it hits walls also, but walls aren't a trigger because i need them to have collisions with the player object
        }
    }

    private void EnableCollider()
    {
        objCollider.enabled = true;
    }

}
