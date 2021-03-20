using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMover : MonoBehaviour
{
    public Transform reference;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - reference.position;
    }

    void Update()
    {
        transform.position = reference.position + offset;   
    }
}
