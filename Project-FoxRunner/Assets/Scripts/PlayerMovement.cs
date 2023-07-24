using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Modifiers")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Ground")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxPositionOffset;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D rb;
    private Animator _animator;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        HandleGroundedState();
        Run();
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

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + new Vector3(boxPositionOffset.x, boxPositionOffset.y, 0), boxSize, 0f, Vector2.down, maxDistance, layerMask);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((transform.position + new Vector3(boxPositionOffset.x, boxPositionOffset.y, 0)) + Vector3.down * maxDistance, boxSize);
    }

    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }
}
