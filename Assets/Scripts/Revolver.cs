using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Bullet
{
    [SerializeField] int damage = 1;
    
    protected override void Impact(Collision collision)
    {
        Health hp = collision.gameObject.GetComponent<Health>();
        if (impactParticles != null)
        {
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity);
        }
        if (impactSound != null)
        {
            AudioHelper.PlayClip2D(impactSound, 1f);
        }
        
        if ( hp != null) {
            hp.ChangeHealth(-damage);
        }
        Destroy(this.gameObject);
    }

} 
