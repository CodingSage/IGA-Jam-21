using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    
    public Transform target;

    public Vector3 offset;

    private Vector3 cameraSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.position = target.position + offset;
        rb.MovePosition(target.position + offset);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraSpeed, smoothSpeed);

        //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraSpeed, smoothSpeed);

        rb.MovePosition(newPosition);
    }
}
