using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    
    public Transform target;

    public Vector3 offset;

    private Vector3 cameraSpeed;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraSpeed, smoothSpeed);
    }
}
