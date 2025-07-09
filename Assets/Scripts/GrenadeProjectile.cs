using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float SpawnProtection = 1f;
    private Collider2D objCollider;
    [SerializeField] private GameObject ball;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objCollider = GetComponent<Collider2D>();
        rb.linearVelocityY = 5f;
        rb.linearVelocityX = 0f;
        objCollider.enabled = true; // temporarily disables upon awaken
        Invoke("EnableCollider", SpawnProtection); //xz waits 1 second before reenabling collider

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float xPos = 2;
        Vector3 worldPos = new Vector3(1, 1, 1);
        GameObject obj = Instantiate(ball, worldPos, Quaternion.identity);
    }
}
