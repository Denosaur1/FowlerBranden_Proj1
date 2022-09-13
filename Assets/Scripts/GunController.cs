using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] float Speed = .25f;
    [SerializeField] GameObject gun = null;
    [SerializeField] GameObject curLock = null;
    Rigidbody rb = null;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        TurnGun();
    }

    private void MovePlayer() 
    {
        float verticalMoveAmountThisFrame = Input.GetAxis("Vertical") * Speed;
        // create a vector from amount and direction
        Vector3 verticalMoveOffset = transform.forward * verticalMoveAmountThisFrame;
        // apply vector to the rigidbody
        rb.MovePosition(rb.position + verticalMoveOffset);
        // technically adjusting vector is more accurate! (but more complex)
        float horizontalMoveAmountThisFrame = Input.GetAxis("Horizontal") * Speed;
        // create a vector from amount and direction
        Vector3 horizontalMoveOffset = transform.right * horizontalMoveAmountThisFrame;
        // apply vector to the rigidbody
        rb.MovePosition(rb.position + horizontalMoveOffset);
        // technically adjusting vector is more accurate! (but more complex)

    }
    private void TurnGun() {

        if (curLock != null)
        {

            gun.transform.LookAt(curLock.transform);
        }
        else {
            gun.transform.rotation.Set(0,0,0,0);

        }
    
    }
}