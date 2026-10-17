using UnityEngine;

public class PlayerMove : MonoBehaviour 
{
    private InputSystem_Actions input;

    private Rigidbody2D rb;
    public Vector2 inputVector { get; private set; }
    public float speed = 5f;

    private Animator anime;
    private SpriteRenderer playerSprite;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        input = InputManager.Instance.input;
        rb = GetComponent<Rigidbody2D>();

        anime = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDead) {  return; }
        inputVector = input.Player.Move.ReadValue<Vector2>();
        if (inputVector.x > 0)
        {
            playerSprite.flipX = false;
        }
        else if (inputVector.x < 0)
        {
            playerSprite.flipX = true;
        }

        anime.SetBool("Run", inputVector != Vector2.zero);
    }

    void FixedUpdate()
    {
        if (isDead) 
        {
            rb.linearVelocity = Vector2.zero;
            return; 
        }

        if (inputVector != Vector2.zero)
        {
            inputVector = inputVector.normalized;
            rb.linearVelocity = inputVector * speed;
            PlayerSoundManager.Instance.PlayerMoveSound();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            PlayerSoundManager.Instance.StopPlayerMoveSound();
        }
    }
}
