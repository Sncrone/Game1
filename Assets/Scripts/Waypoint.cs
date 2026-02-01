using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waypointDistance = 0.3f;
    
    private int currentWaypoint = 0;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Make sure rigidbody is kinematic so it doesn't fall or get affected by physics
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    
    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        
        Patrol();
    }
    
    private void Patrol()
    {
        // Get target position
        Vector2 targetPos = waypoints[currentWaypoint].position;
        
        // Move towards target
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        
        // Apply movement
        if (rb != null)
        {
            rb.MovePosition(newPos);
        }
        else
        {
            transform.position = newPos;
        }
        
        // Check if reached waypoint
        if (Vector2.Distance(transform.position, targetPos) < waypointDistance)
        {
            // Go to next waypoint
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawWireSphere(waypoints[i].position, 0.3f);
                
                int nextIndex = (i + 1) % waypoints.Length;
                if (waypoints[nextIndex] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[nextIndex].position);
                }
            }
        }
    }
}