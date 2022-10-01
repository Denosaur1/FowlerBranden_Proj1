using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
   
    [SerializeField] public float speed = 1f;
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] AudioClip impactSound;
    GameObject Player;
   
    Rigidbody rb;
    Health hp;
    void Awake()
    {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player");
        
    }
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            //PlayerImpact(player);
            ImpactFeedback();
            
        
        }

    }
    /*
    protected virtual void PlayerImpact(Player player) {
        player.DecreaseHealth(damageAmount);
    
    }
    */
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
        
    }
    public void Move()
    {
        transform.LookAt(Player.transform);
        transform.position =Vector3.MoveTowards(transform.position,Player.transform.position, speed *Time.deltaTime);
    }
    


}