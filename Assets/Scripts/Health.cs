using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int curHealth = 0;
    public int maxHealth = 10;
    public ParticleSystem deathParticles;
    public AudioClip deathSound;
    public ParticleSystem hurtParticles;
    public AudioClip hurtSound;
    [SerializeField] DeathEffect deathEffect;
    public void Awake()
    {
       
        curHealth = maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        curHealth += amount;
        
        Debug.Log(gameObject.name + "'s Current Heath: " + curHealth);
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        if (amount < 0) {
            if (hurtParticles != null)
            {
                Instantiate(hurtParticles, transform.position, Quaternion.identity);
            }
            if (hurtSound != null)
            {
                AudioHelper.PlayClip2D(hurtSound, 1f);
            }


        }
        if (curHealth <=0) { Kill(); }

    }
    
    public void Kill()
    {
        
        if (deathParticles != null)
        {
            deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        if (deathSound != null)
        {
            AudioHelper.PlayClip2D(deathSound, 1f);
        }
        if (deathEffect != null)
        {
            deathEffect.Death();

        }
        else {
            gameObject.SetActive(false);
        }
    }

}
