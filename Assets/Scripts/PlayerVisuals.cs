// PlayerVisuals.cs (Attached to Player GameObject)
using System.Collections;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine currentFlashRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void FlashRedTemporarily(float duration, GameObject projectileToDestroy = null)
    {
        if (spriteRenderer == null) return;

        if (currentFlashRoutine != null)
        {
            StopCoroutine(currentFlashRoutine);
            spriteRenderer.color = originalColor;
        }
        currentFlashRoutine = StartCoroutine(DoFlashRedRoutine(duration, projectileToDestroy));
    }

    private IEnumerator DoFlashRedRoutine(float duration, GameObject projectileToDestroy) // the actual coroutine that does the flash red 
    {
        spriteRenderer.color = Color.red;
        if (projectileToDestroy != null)
        {
            Destroy(projectileToDestroy);
        }

        yield return new WaitForSeconds(duration);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }


        currentFlashRoutine = null;
    }
}