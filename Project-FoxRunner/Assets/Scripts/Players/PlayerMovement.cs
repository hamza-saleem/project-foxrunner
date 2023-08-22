using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [Header("Modifiers")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float deathSpeed = 2f;

    [Header("Ground")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxPositionOffset;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D rb;
    private Animator _animator;
    private SpriteRenderer _sprite;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isDead;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        _animator.SetBool("isIdle", true);
    }

    private void Update()
    {
        if(GameManager.Instance.gameStarted && !GameManager.Instance.GameOver())
        {
            HandleGroundedState();
            Run();

        }

        if (GameManager.Instance.GameOver())
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            PlayerDeath();
        }
    }

    private void HandleGroundedState()
    {
        isGrounded = IsGrounded();
        _animator.SetBool("isRunning", isGrounded);
        _animator.SetBool("isJumping", !isGrounded);
    }

    private void Run()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Jump()
    {

        if (PlayerPrefs.GetString("FirstPlay") == "Yes")
            PlayerPrefs.SetString("FirstPlay", "No");

        if (isGrounded)
        {
           rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + new Vector3(boxPositionOffset.x, boxPositionOffset.y, 0), boxSize, 0f, Vector2.down, maxDistance, layerMask);
        return hit.collider != null;
    }


    private void PlayerDeath()
    {
        rb.bodyType = RigidbodyType2D.Static;
        _animator.SetBool("isDead", true);
        transform.Translate(Vector2.up * deathSpeed * Time.deltaTime);
        StartCoroutine(FadeOutSprite());
    }

    private IEnumerator FadeOutSprite()
    {
        // Get the initial color of the sprite
        Color initialColor = _sprite.color;

        float elapsedTime = 0f;
        float fadeDuration = 1.5f; // Adjust the duration as needed

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the new alpha value based on the elapsed time and fade duration
            float newAlpha = Mathf.Lerp(initialColor.a, 0f, elapsedTime / fadeDuration);

            // Set the new alpha value for the sprite color
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, newAlpha);
            _sprite.color = newColor;

            yield return null;
        }
        yield return new WaitForSeconds(1.25f);

        UIManager.Instance.OnDeath();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((transform.position + new Vector3(boxPositionOffset.x, boxPositionOffset.y, 0)) + Vector3.down * maxDistance, boxSize);
    }

    public Vector3 GetPosition() => transform.localPosition;

    public bool IsDead() => isDead;
}
