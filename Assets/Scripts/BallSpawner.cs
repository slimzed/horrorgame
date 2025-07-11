using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform childContainer;
    [SerializeField] private Transform enemyHitbox;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float initialDelay = 1f;

    private SpriteRenderer spriteRenderer;

    // code that finds the maximum and minimum x values of the roof
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        minX = spriteRenderer.bounds.min.x + 1f;
        maxX = spriteRenderer.bounds.max.x - 1f;
        
        
        InvokeRepeating("SpawnObject", initialDelay, spawnInterval);
    }

    private void SpawnObject()
    {
        int shouldSpawn = Random.Range(0, 2); // randomly picks a number from 0 to 1 as to whether the ball spawner should spawn an objects
        if (ball && shouldSpawn == 1)
        {
            float xPos = Random.Range(minX, maxX);
            Vector3 worldPos = new Vector3(xPos, gameObject.transform.position.y, childContainer.position.z);
            GameObject obj = Instantiate(ball, worldPos, Quaternion.identity.normalized);
            obj.transform.SetParent(childContainer.transform);
            
            
            if (childContainer.localScale != Vector3.one) // if the scale on childcontainer > 1, then the objects spawn weirdly so this is just a check for that 
            {
                Debug.LogWarning("childContainer is out of scale");
            }
        }



    }

}
