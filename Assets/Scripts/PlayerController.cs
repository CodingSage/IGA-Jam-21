using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Collider levelExitTrigger;
    public Animator animator;
    public AudioManager audioManager;

    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float pushPower = 2f;

    private CharacterController controller;
    private Vector3 previousFramePos;
    private float stepOffset;
    private bool dragging;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        previousFramePos = transform.position;
        stepOffset = controller.stepOffset;
        dragging = false;

        if (levelExitTrigger == null)
        {
            Debug.LogError("Level Exit Trigger must be set on Player object!");
        }
    }

    private void LateUpdate()
    {
        //if (!Input.GetKey(KeyCode.Space))
        //{
        //    dragging = false;
        //}
    }

    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Disable stepping if push action is in progress
        controller.stepOffset = Input.GetKey(KeyCode.Space) ? 0f : stepOffset;

        Vector3 direction = new Vector3(hAxis, 0.0f, vAxis);

        if (direction.magnitude > 0.0f)
        {
            controller.SimpleMove(direction * moveSpeed);
        }

        Vector3 moveDistance = transform.position - previousFramePos;

        if (moveDistance.magnitude > 0.05f)
        {
            if (!audioManager.WalkingSoundPlaying)
            {
                audioManager.PlayWalkingSound();
            }
        }
        else
        {
            audioManager.StopWalkingSound();
        }

        previousFramePos = transform.position;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (// Push action not enabled
            !Input.GetKey(KeyCode.Space)
            // no rigidbody
            || body == null || body.isKinematic
            // We dont want to push objects below us
            || hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (levelExitTrigger != null)
        {
            if (other == levelExitTrigger)
            {
                //animator.SetTrigger("FadeOut");
                animator.SetInteger("LevelIndex", SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
