using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class ControllableCharacter : MonoBehaviour
{
    public bool IsControlled { get; private set; }

    private PlayerController controller;
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;
    private Collider2D col;
    private Rigidbody2D rb;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeControl()
    {
        IsControlled = true;

        if (controller)
            controller.enabled = true;

        if (spriteRenderer)
            spriteRenderer.enabled = true;

        if (playerInput)
            playerInput.enabled = true;

        if (col)
            col.enabled = true;
            
        if (rb)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        
        // Re-enable respawn script if it exists
        RespawnOnFall respawn = GetComponent<RespawnOnFall>();
        if (respawn != null)
            respawn.enabled = true;
    }

    public void ReleaseControl(bool hideCharacter)
    {
        IsControlled = false;

        if (controller)
            controller.enabled = false;

        if (spriteRenderer && hideCharacter)
            spriteRenderer.enabled = false;

        if (playerInput)
            playerInput.enabled = false;

        if (hideCharacter)
        {
            // Completely disable the object
            if (col)
                col.enabled = false;
                
            if (rb)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.zero;
            }
            
            // Disable respawn script to stop the loop
            RespawnOnFall respawn = GetComponent<RespawnOnFall>();
            if (respawn != null)
                respawn.enabled = false;
        }
        else
        {
            // Just make it kinematic but keep collision
            if (rb)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}