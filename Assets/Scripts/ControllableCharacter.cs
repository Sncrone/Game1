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

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        col = GetComponent<Collider2D>();
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

        if (col)
            col.enabled = false;
    }
}
