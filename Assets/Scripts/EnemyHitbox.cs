using UnityEngine;
using System;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private Sprite stage2;
    [SerializeField] private Sprite stage1;
    private SpriteRenderer spriteRenderer;

    private int itsHealth;

    // these are actions that will be sent to StatTracker to verify the number of enemies.
    void Start() // adds to the remaining enemies, MUST BE IN START otherwise StatTracker.Instance will NOT BE FOUND 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StatTracker.Instance.AddRemainingEnemies();
        itsHealth = StatTracker.Instance.GetHealth();
        DontDestroyOnLoad(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(collider.gameObject);
        if (collider.transform.CompareTag("Projectile")) // checks if the collided object is a projectile
        {
            itsHealth--;
            if (itsHealth <= 0)
            {
                gameObject.SetActive(false); // toggles off the hitbox when killed, could also be Destroy()
                StatTracker.Instance.SubtractRemainingEnemies();
                // loads into next scene or in between screen
            }
            else if (itsHealth <= 4)
            {
                spriteRenderer.color = Color.red;
                // spriteRenderer.sprite = stage2;
            }
            else if (itsHealth <= 7)
            {
                spriteRenderer.color = Color.yellow;
                // spriteRenderer.sprite = stage1;
                
            }
        }
    }
}
