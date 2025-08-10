using UnityEngine;

/// <summary>
/// Simple 2D camera follow with smoothing. Attach to Main Camera.
/// </summary>
public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.12f;

    private Vector3 _vel;

    private void LateUpdate()
    {
        if (!target) return;
        var desired = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref _vel, smoothTime);
    }
}