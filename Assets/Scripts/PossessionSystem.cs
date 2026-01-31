using UnityEngine;
using UnityEngine.InputSystem;

public class PossessionSystem : MonoBehaviour
{
    [SerializeField] private float possessionRange = 2f;
    [SerializeField] private LayerMask npcLayer;
    [SerializeField] private int maxPossessions = 3;
    [SerializeField] private CameraFollow cameraFollow;

    private ControllableCharacter currentCharacter;
    private int possessionCount;

    public void SetInitialCharacter(ControllableCharacter startCharacter)
    {
        currentCharacter = startCharacter;
        currentCharacter.TakeControl();

        cameraFollow.SetTarget(currentCharacter.transform);
    }

    public void OnPossess(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (possessionCount >= maxPossessions) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            currentCharacter.transform.position,
            possessionRange,
            npcLayer
        );

        ControllableCharacter newCharacter = null;

        foreach (var hit in hits)
        {
            var candidate = hit.GetComponent<ControllableCharacter>();

            if (!candidate) continue;
            if (candidate.IsControlled) continue;
            if (candidate == currentCharacter) continue;

            newCharacter = candidate;
            break;
        }

        if (!newCharacter) return;

        // eski bedeni býrak
        currentCharacter.ReleaseControl(hideCharacter: true);

        // yeni bedeni al
        currentCharacter = newCharacter;
        currentCharacter.TakeControl();

        cameraFollow.SetTarget(currentCharacter.transform);

        possessionCount++;
    }

}
