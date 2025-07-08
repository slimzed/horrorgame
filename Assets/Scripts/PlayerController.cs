using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject playerKnife;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerInput playerInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.Enable();
        playerKnife.SetActive(false);
    }
    private void OnEnable()
    {
        // StatTracker.OnGameOver += HandleGameOver;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);
        
    }
    public void OnAttack() // function called when LeftClick is pressed.
    {
        playerKnife.SetActive(true);
        Invoke("HideKnife", 1f); // hides knife after 1s
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

        // change this to show a restart screen 
        Invoke("Restart", 2f);

    }

    void Restart()
    {
        gameObject.SetActive(true);
        // StatTracker.Instance.SetLives(5);
    }

}
