using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private float fallThreshold = -10f; // Y position to trigger respawn
    [SerializeField] private Vector3 respawnPosition = Vector3.zero; // Where to respawn
    
    [Header("Optional - Set Respawn Point")]
    [SerializeField] private Transform respawnPoint; // Drag a GameObject here to use its position
    
    void Update()
    {
        // Check if player fell below threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }
    
    void Respawn()
    {
        // Use respawn point if set, otherwise use respawnPosition
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            transform.position = respawnPosition;
        }
        
        // Reset velocity if you have a Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        
        Debug.Log("Player respawned!");
    }
}