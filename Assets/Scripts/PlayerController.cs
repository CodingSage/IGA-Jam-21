using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //new Rigidbody rigidbody;

    //public float movementSpeed = 100.0f;
    //public float jumpSpeed = 100.0f;

    //private bool jumping;
    //private Vector3 targetVelocity;

    //void Start()
    //{
    //    rigidbody = GetComponent<Rigidbody>();
    //    jumping = false;
    //    targetVelocity = Vector3.zero;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    ProcessMove();

    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        ProcessJump();
    //    }

    //    //ApplyVelocity(targetVelocity);
    //}

    //private void ProcessMove()
    //{
    //    //targetVelocity.x = Input.GetAxisRaw("Horizontal");
    //    //targetVelocity.z = Input.GetAxisRaw("Vertical");

    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //    rigidbody.AddForce(movement);
    //}

    //private void ProcessJump()
    //{
    //    if (!jumping)
    //    {
    //        jumping = true;

    //        rigidbody.AddForce(new Vector3(0.0f, jumpSpeed, 0.0f));
    //    }
    //}

    //private void ApplyVelocity(Vector3 targetVelocity)
    //{
    //    rigidbody.velocity = (targetVelocity * movementSpeed) * Time.deltaTime;

    //    rigidbody.velocity = new Vector3(
    //            targetVelocity.x * movementSpeed * Time.deltaTime,
    //            rigidbody.velocity.y,
    //            targetVelocity.z * movementSpeed * Time.deltaTime);
    //}

    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float gravityAcceleration = 9.81f;
    private float ySpeed;
    private float fallTime;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        ySpeed = 0.0f;
        fallTime = 0.0f;
    }

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            ySpeed = 0.0f;
            fallTime = 0.0f;
        }
        else
        {
            fallTime += Time.deltaTime;
            ySpeed -= gravityAcceleration * fallTime;
        }

        Vector3 direction = new Vector3(hAxis, ySpeed, vAxis);

        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}
