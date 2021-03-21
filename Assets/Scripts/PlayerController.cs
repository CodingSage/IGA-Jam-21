using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Collider levelExitTrigger;
    public Animator animator;

    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float pushPower = 2f;

    //[SerializeField]
    //private float gravityAcceleration = 9.81f;
    //private float ySpeed;
    //private float fallTime;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //ySpeed = 0.0f;
        //fallTime = 0.0f;

        if (levelExitTrigger == null)
        {
            Debug.LogError("Level Exit Trigger must be set on Player object!");
        }
    }

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        //if (controller.isGrounded)
        //{
        //    ySpeed = 0.0f;
        //    fallTime = 0.0f;
        //}
        //else
        //{
        //    fallTime += Time.deltaTime;
        //    ySpeed -= gravityAcceleration * fallTime;
        //}

        //Vector3 direction = new Vector3(hAxis, ySpeed, vAxis);

        Vector3 direction = new Vector3(hAxis, 0.0f, vAxis);

        controller.SimpleMove(direction * moveSpeed);
    }

    public void LoadNextLevel()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < sceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
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
                animator.SetTrigger("FadeOut");
            }
        }
    }
}
