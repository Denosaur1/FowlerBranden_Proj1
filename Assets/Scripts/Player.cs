using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GunController))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathSound;
  
    //public bool damageProof = false;
  
   
  
    
    [SerializeField] public GameObject[] playerArt; 
    [SerializeField] public Material[] playerMat;

    /*
    [SerializeField] TextMeshProUGUI pHealth;
    [SerializeField] TextMeshProUGUI pScore;
    [SerializeField] TextMeshProUGUI pSpeed;
    */
    Health health= null;
    private void Awake()
    {
       
        playerMat = new Material[playerArt.Length];
        for (int i = 0; i < playerArt.Length; i++) {
            playerMat[i] = playerArt[i].GetComponent<MeshRenderer>().material;
        
        }
        health = GetComponent<Health>();
        if (deathParticles != null) { deathParticles = health.deathParticles; }
        if (deathSound != null) {deathSound = health.deathSound;}
        
    }

   

    
    /*
   public void IncreaseHealth(int amount) {
       curHealth += amount;
       curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
       Debug.Log("Player Health:" + curHealth);

   }

   public void IncreaseScore(int amount) {
       score += amount;

       Debug.Log("Score:" + score);

   }
   public void DecreaseHealth(int amount)
   {
       if (!damageProof) { 
           curHealth -= amount;
           Debug.Log("Player Health:" + curHealth);
           if (curHealth <= 0)
           {
               Kill();

           }
       }
   }
   public void Kill() {
       gameObject.SetActive(false);
       //TODO sound and particles
       if (deathParticles != null)
       {
           deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
       }
       if (deathSound != null)
       {
           AudioHelper.PlayClip2D(deathSound, 1f);
       }
   }
   */

}


