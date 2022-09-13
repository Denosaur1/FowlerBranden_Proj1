using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class PowerUpBase : MonoBehaviour
{
  [SerializeField] float powerupDuration;
    protected float PowerupDuraion => powerupDuration;
    [SerializeField] float movementSpeed = 1;
    protected float MovementSpeed => movementSpeed;
    [SerializeField] ParticleSystem powerParticles;
    [SerializeField] AudioClip powerSound;
    Rigidbody rb;
    Collider cd;
    MeshRenderer mr;
    protected abstract void PowerUp(Player player, float duration);
    protected virtual void PowerDown(Player player)
    {
        
        gameObject.SetActive(false);

    }
   
     private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cd = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
    }
    private void FixedUpdate()
    {
        Movement(rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {

        Quaternion turnOffset = Quaternion.Euler(0, movementSpeed, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player, powerupDuration);
            Feedback();
            cd.enabled = !cd.enabled;
            mr.enabled = !mr.enabled;
            


        };
    }
    private void Feedback()
    {
        if (powerParticles != null)
        {
            powerParticles = Instantiate(powerParticles, transform.position, Quaternion.identity);
        }
        if (powerSound != null)
        {
            AudioHelper.PlayClip2D(powerSound, 1f);
        }

    }
}
