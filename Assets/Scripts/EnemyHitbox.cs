using UnityEngine;
using System;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private Sprite stage2;
    [SerializeField] private Sprite stage1;
    [SerializeField] private AudioClip EnemyHitSfx;
    private SpriteRenderer spriteRenderer;


    private GameObject audioManager;
    private AudioSource source;
    
    
    
    private int itsHealth;

    // these are actions that will be sent to StatTracker to verify the number of enemies.
    void Start() // adds to the remaining enemies, MUST BE IN START otherwise StatTracker.Instance will NOT BE FOUND 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = GameObject.Find("ScriptCalls");
        source = audioManager.GetComponent<AudioSource>();
        
        StatTracker.Instance.AddRemainingEnemies();    
        itsHealth = StatTracker.Instance.GetHealth();
        
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(collider.gameObject);
        if (collider.transform.CompareTag("Projectile")) // checks if the collided object is a projectile
        {
            itsHealth--;

            source.clip = EnemyHitSfx;
            source.Play();



            if (itsHealth <= 1)
            {
                Destroy(gameObject);
                StatTracker.Instance.SubtractRemainingEnemies();
            }
            else if (itsHealth <= 2)
            {
                spriteRenderer.color = Color.red;
                // spriteRenderer.sprite = stage2;
            }
            else if (itsHealth <= 3)
            {
                spriteRenderer.color = Color.yellow;
                // spriteRenderer.sprite = stage1;
                
            }
        }
    }
}
