using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Bullet
{
    [SerializeField] int damage = 1;
    Enemy hitEnemy = null;
    protected override void Impact(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            //TODO add damage to boss and pillars once implemented
        }
    }

} 
