using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private Sprite stage2;
    [SerializeField] private Sprite stage1;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collision");
        if (collider.CompareTag("Projectile")) // checks if the collided object is a projectile
        {
            health--;
            Debug.Log(health);
            if (health == 0)
            {
                gameObject.SetActive(false); // toggles off the hitbox when killed, could also be Destroy()

                // loads into next scene or in between screen
            }
            else if (health <= 4)
            {
                spriteRenderer.color = Color.red;
                // spriteRenderer.sprite = stage2;
            }
            else if (health <= 7)
            {
                spriteRenderer.color = Color.yellow;
                // spriteRenderer.sprite = stage1;
                Destroy(gameObject);
                
                // load into the menu where you can pick power boost or whatever
            }
        }
    }
}
