using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLock : MonoBehaviour
    
{
    Rigidbody rb;
    [SerializeField]float xSpeed;
    [SerializeField]float ySpeed;
    [SerializeField]float zSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Movement(rb);
    }
    protected virtual void Movement(Rigidbody rb)
    {

        Quaternion turnOffset = Quaternion.Euler(xSpeed, ySpeed, zSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

   
}
