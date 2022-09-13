using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] public int heath = 1;
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] AudioClip impactSound;

    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            PlayerImpact(player);
            ImpactFeedback();
        
        
        }

    }
    protected virtual void PlayerImpact(Player player) {
        player.DecreaseHealth(damageAmount);
    
    }
    private void ImpactFeedback() {

        if (impactParticles != null) { 
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity); 
        }
        if (impactSound != null) {
            AudioHelper.PlayClip2D(impactSound, 1f);
        }
    
    }

    private void FixedUpdate()
    {
        Move();
        if(heath < 0) { gameObject.SetActive(false); }
    }
    public void Move()
    {
        
    }
    


}