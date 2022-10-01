using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;



public class Enemy : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
  
   
    [SerializeField] public float speed = 1f;
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] AudioClip impactSound;
    [SerializeField] CameraShake cameraShake;
    GameObject Player;
   
    Rigidbody rb;
    Health hp;
    NavMeshAgent agent;
    void Awake()
    {
        hp = GetComponent<Health>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        agent.speed = speed;
        //agent.destination = (Player.transform.position);
        //agent.baseOffset = 0;

    }
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            //PlayerImpact(player);
            ImpactFeedback();
            Health hp = player.GetComponent<Health>();
            if (hp != null)
            {
                hp.ChangeHealth(-damageAmount);
                if (cameraShake)
                {
                    StartCoroutine(cameraShake.Shake(.5f, .1f));

                }

            }

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
        if (agent.baseOffset > 0.6f)
        {
            agent.baseOffset = Mathf.Lerp(agent.baseOffset, 0.5f, .05f);


        }


        else { Move(); }
        
    }
    public void Move()
    {
        agent.destination = (Player.transform.position);
    }
    


}