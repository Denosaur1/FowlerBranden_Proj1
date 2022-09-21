using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class Boss : MonoBehaviour
{
    string BossState = "Moving";
    float actionTime = 10f;
    float actionTimer = 0f;
    
    Health health = null;
    [SerializeField] int maxHealth = 10;
    [SerializeField] float speed = 1f;
    [SerializeField] float curSpeed = 1f;
    Vector3 chargeTarget;
    int curHealth;
    bool Enrage = false;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathSound;
    [SerializeField] GameObject Jaw = null;
    [SerializeField] Transform target = null;
    private void Awake()
    {

        curSpeed = speed;
        health = GetComponent<Health>();
        if (deathParticles != null) { health.deathParticles = deathParticles; }
        if (deathSound != null) { health.deathSound = deathSound; }
        health.maxHealth = maxHealth;
    }

    private void FixedUpdate()
    {

        curHealth = health.curHealth;
        if(curHealth < (maxHealth / 2)) { Enrage = true; }


        if (Enrage) { 
            Jaw.SetActive(false);
            curSpeed = 2 * speed;      }
        if (actionTimer >= 0)
        {
            if (actionTimer < actionTime)
            {
                actionTimer += Time.deltaTime;
            }
            else {
                actionTimer = -1;
                RandomizeAction();
               
            }
        }
        if (BossState == "Moving") {
            transform.LookAt(target);
            MoveToPlayer();
            
        
        }
        if (BossState == "Charging") {
            transform.LookAt(target);

            ChargeToPlayer();
            
        
        }

    }
    void RandomizeAction() {
        int choice = Random.Range(0,10);
        if (choice < 1) { BossState = "Moving";
            actionTimer = 0;
        }
        else { BossState = "Charging";
            chargeTarget = target.position;
        }
        Debug.Log("Action Taken: " + BossState +" With Choice Value of: " + choice);
       

    }
    void MoveToPlayer() { transform.position = Vector3.MoveTowards(transform.position, target.position, curSpeed); }
    void ChargeToPlayer() {
       

       
            transform.position = Vector3.MoveTowards(transform.position, chargeTarget, 3 * curSpeed);
        if (transform.position == chargeTarget ) {


            BossState = "Moving";
            actionTimer = 7;
        }
        
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (BossState == "Charging") {
            Debug.Log("Collided");

            BossState = "Moving";
            actionTimer = 7;
            
        }
    }
}
