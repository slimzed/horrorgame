using UnityEngine;
using System;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private Sprite stage2;
    [SerializeField] private Sprite stage1;
    private SpriteRenderer spriteRenderer;
    public static event Action AddEnemy;
    public static event Action RemoveEnemy;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AddEnemy?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(collider.gameObject);
        if (collider.transform.CompareTag("Projectile")) // checks if the collided object is a projectile
        {
            health--;
            if (health == 0)
            {
                gameObject.SetActive(false); // toggles off the hitbox when killed, could also be Destroy()
                RemoveEnemy?.Invoke();
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
