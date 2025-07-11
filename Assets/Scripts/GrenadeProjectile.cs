using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private Sprite grenadeSprite;
    [SerializeField] private GameObject grenadeHitboxPrefab;
    
    
    private Rigidbody2D rb;
    private Collider2D objCollider;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        rb.linearVelocityY = 5f;
        rb.linearVelocityX = 0f;
        objCollider.enabled = false; // temporarily disables upon awaken

        anim.enabled = false;


        Invoke("EnableCollider", 1);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.enabled = true;
        GameObject grenadeHitbox = Instantiate(grenadeHitboxPrefab, transform.position, Quaternion.identity);
        grenadeHitbox.transform.SetParent(gameObject.transform);
        rb.linearVelocity = Vector2.zero; // make sure that the grenaed stands still while the animation is playing.
        Invoke("AnimDisable", 0.1f);
        // enemyhitbox actually does the checking if its grenade and removes health 
    }
    private void AnimDisable()
    {
        anim.enabled = false;
        Destroy(gameObject);
    }
    private void EnableCollider()
    {
        objCollider.enabled = true;
    }
}
