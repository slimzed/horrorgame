using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject playerKnife; // sets the playerknife to an actual game obejct
    [Tooltip("This decides how long the wait will be between successive parries")]
    [SerializeField] private float KnifeDebounceTime = 0.5f; // sets the debounce on the knife input
    [Tooltip("This decides how long the knife will be shown")]
    [SerializeField] private float KnifeShownTime = 1f;
    [Tooltip("This decides how long the animation will take for the knife.")]
    [SerializeField] private float KnifeAnimationTime = 0.1f;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite midSprite;
    
    
    
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerInput playerInput;
    private Vector3 playerKnifeTransformLocal; // creates a vector3 that is local (in regards to) the whole player object
    private SpriteRenderer spriteRenderer;

    private float lastInputTime;
    

    void Start()
    {
        if (leftSprite == null)
        {
            Debug.LogWarning("leftSprite is not set.");
        }
        if (rightSprite == null)
        {
            Debug.LogWarning("rightSprite is not set.");
        }
        if (playerKnife == null)
        {
            Debug.LogWarning("playerKnife is not set.");
        }


            rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.Enable();
        playerKnife.SetActive(false);
        lastInputTime = -KnifeDebounceTime; // just set it to a random small value so that first input will always occur :D

        playerKnifeTransformLocal = playerKnife.transform.localPosition;


    }
    private void OnEnable()
    {
        StatTracker.OnGameOver += HandleGameOver;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);
        
    }
    public void OnAttack() // function called when LeftClick is pressed.
    {
        if (Time.time - lastInputTime >= KnifeDebounceTime + KnifeShownTime)
        {
            lastInputTime = Time.time;
            playerKnife.SetActive(true);
            StartCoroutine(moveKnife());
        }
        
    }
    /// <summary>
    /// This function is a little confusing so I'll explain it here. Essentially what we are doing is creating a coroutine (a function that can CHOOSE when to return) and 
    /// using this coroutine to lerp the knife animation over time. This basically gives the script some time to play the knife animation, otherwise it would simply 
    /// teleport. Further, we are using the playerKnifeTransformLocal to store the knife's x value respective to the player, ensuring that the knife is tracking the player at all 
    /// times because it is moving RESPECTIVE TO the player. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator moveKnife()
    {
        Vector3 startPos = new Vector3(playerKnifeTransformLocal.x, playerKnife.transform.localPosition.y, playerKnife.transform.localPosition.z);
        Vector3 endPos = new Vector3(playerKnifeTransformLocal.x, playerKnifeTransformLocal.y + 0.5f, playerKnifeTransformLocal.z);
        float elapsedTime = 0f;

        while (elapsedTime < KnifeAnimationTime)
        {
            Vector3 interpolatedPos = Vector3.Lerp(startPos, endPos, elapsedTime / KnifeAnimationTime);

            playerKnife.transform.localPosition = new Vector3(playerKnifeTransformLocal.x, interpolatedPos.y, interpolatedPos.z); // makes sure that the object is STILL the same x position local, but is interpolating both y and z
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerKnife.transform.localPosition = new Vector3(playerKnifeTransformLocal.x, endPos.y, endPos.z);

        yield return new WaitForSeconds(KnifeShownTime); // currently just made it a random value, this is how long the knife will be shown for before hiding 

        HideKnife();
     }

    private void HideKnife()
    {
        playerKnife.SetActive(false);
        playerKnife.transform.localPosition = playerKnifeTransformLocal; // makes sure that the knife 
    }


    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        if (leftSprite != null  && moveInput.x > 0)
        {
            spriteRenderer.sprite = leftSprite;
        } else if (rightSprite != null && moveInput.x < 0)
        {
            spriteRenderer.sprite = rightSprite;
        } else if (midSprite != null && moveInput.x == 0)
        {
            spriteRenderer.sprite = midSprite;
        }
    }

    void HandleGameOver()
    {
        Debug.Log("Game over...");
        gameObject.SetActive(false); // optional: disable player for a second

    }


    void Restart() // change this function later so that it actually does something important
    {
        gameObject.SetActive(true);
        StatTracker.Instance.SetLives(10);
    }

}
