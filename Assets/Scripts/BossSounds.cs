using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{

    [SerializeField] Transform placement;
    [SerializeField] Boss bossController;
    [SerializeField] public int damage;
    void SoundAction(AudioClip sound) {

        AudioHelper.PlayClip2D(sound, 1f);


    }

    void HitCheck(float r) {
        bool playerHit = false;
        Collider[] hitColliders = Physics.OverlapSphere(placement.position, r);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.name == "Player" && !playerHit)
            {
                Debug.Log("PLAYER HIT");
                bossController.HitPlayer(hitCollider.gameObject, damage);
                playerHit = true;

            }


        }

    }
    void ParticleEffect(ParticleSystem particle) {

        Instantiate(particle, placement.position, Quaternion.identity);


    }  
    


}
