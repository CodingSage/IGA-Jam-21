using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Rigidbody rigidbody;

    public float movementSpeed = 500.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        Move(targetVelocity);
    }

    private void Move(Vector3 targetVelocity)
    {
        rigidbody.velocity = (targetVelocity * movementSpeed) * Time.deltaTime;
    }
}
