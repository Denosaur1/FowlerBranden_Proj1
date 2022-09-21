using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected abstract void Impact(Collision otherCollision);

    [Header("Base Settings")]
    [SerializeField] protected float TravelSpeed = .25f;
    [SerializeField] protected float TravelTime = .25f;
    [SerializeField] protected ParticleSystem impactParticles;
    [SerializeField] protected AudioClip impactSound;
    float timer = 0f;
    [SerializeField] protected Rigidbody RB;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collision!");
        Impact(collision);
    }

    
    private void FixedUpdate()
    {
        if (timer < TravelTime)
        {
            Move();
            timer += Time.deltaTime;
        }
        else { Destroy(this.gameObject); }
    }

    protected virtual void Move()
    {
        Vector3 moveOffset = transform.forward * TravelSpeed;
        RB.MovePosition(RB.position + moveOffset);
    }
}

