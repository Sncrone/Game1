using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth = 5f;

    public void SetTarget(Transform newTarget, bool snap = false)
    {
        if (!newTarget) return;

        target = newTarget;

        if (snap)
        {
            Vector3 pos = target.position;
            pos.z = -10f;
            transform.position = pos;
        }
    }


    private void LateUpdate()
    {
        if (!target) return;

        Vector3 pos = target.position;
        pos.z = -10f;

        transform.position = Vector3.Lerp(
            transform.position,
            pos,
            smooth * Time.deltaTime
        );
    }
}
