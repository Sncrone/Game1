using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask NpcLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private CharacterSounds sounds; // ADD THIS

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sounds = GetComponent<CharacterSounds>(); // ADD THIS
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
        
        // ADD THIS - Play walk sound
        if (sounds != null)
        {
            bool isMoving = Mathf.Abs(moveInput.x) > 0.1f;
            sounds.PlayWalkSound(isMoving, isGrounded);
        }
    }

    // Input System callback
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Input System callback
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!isGrounded) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(
            moveInput.x * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void CheckGround()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            groundCheck.position,
            groundCheckRadius
        );

        isGrounded = false;

        foreach (var hit in hits)
        {
            // Kendi collider'ýmýzý görmezden gel
            if (hit.transform == transform)
                continue;

            // Sadece ground layer
            if (hit.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = true;
                break;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}