using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableBox : MonoBehaviour
{
    [SerializeField] private string allowedTag = "BoxPusher";
    [SerializeField] private float minPushSpeed = 0.1f;

    private Rigidbody2D boxRb;

    private void Awake()
    {
        boxRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(allowedTag))
        {
            boxRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        } else {
            boxRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(allowedTag))
        {
            boxRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        } else {
            boxRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
