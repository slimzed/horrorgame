using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject playerKnife; // sets the playerknife to an actual game obejct
    [SerializeField] private float KnifeDebounceTime = 0.5f; // sets the debounce on the knife input
    [SerializeField] private float KnifeHideTime = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerInput playerInput;


    private float lastInputTime;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.Enable();
        playerKnife.SetActive(false);
        lastInputTime = -KnifeDebounceTime; // just set it to a random small value so that first input will always occur :D
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
        if (Time.time - lastInputTime >= KnifeDebounceTime + KnifeHideTime)
        {
            lastInputTime = Time.time;
            Debug.Log("input registered");
            playerKnife.SetActive(true);
            Invoke("HideKnife", KnifeHideTime); // hides knife after 1s
        }
    }
    private void HideKnife()
    {
        playerKnife.SetActive(false);
    }


    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    void HandleGameOver()
    {
        gameObject.SetActive(false);

        Invoke("Restart", 2f);
        

    }

    void Restart() // change this function later so that it actually does something important
    {
        gameObject.SetActive(true);
        StatTracker.Instance.SetLives(100);
    }

}
