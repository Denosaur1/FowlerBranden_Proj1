using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] float Speed = .25f;
    [SerializeField] GameObject gun = null;
    [SerializeField] GameObject curLock = null;
    [SerializeField] ParticleSystem shootParticles;
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject curGun;
    [SerializeField] GameObject bulletStart;
    Vector3 targetPos;
    Quaternion targetRot;
    Rigidbody rb = null;
    bool locked = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Fire();

        }
        if (Input.GetKeyDown(KeyCode.E)) {
            locked = !locked;
        
        
        }

    }
    void FixedUpdate()
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
        
        if (curLock != null && locked)
        {
            
            Vector3 position = new Vector3(curLock.transform.position.x, 0.5f, curLock.transform.position.z);
            gun.transform.LookAt(position);
        }
        else {
            gun.transform.rotation = Quaternion.Euler(0, 0.5f, 0);

        }
        
        

    }
    public void Fire()
    {
        targetPos = bulletStart.transform.position;
        targetRot = bulletStart.transform.rotation;
        if (shootParticles != null)
        {
            Instantiate(shootParticles, targetPos, targetRot);
        }
        if (shootSound != null)
        {
            AudioHelper.PlayClip2D(shootSound, 1f);
        }
        if (curGun != null)
        {

            Instantiate(curGun, targetPos, targetRot);
        }
    }
}
