using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : DeathEffect
{
    // Start is called before the first frame update
    public override void Death()
    {

       
        Destroy(this.gameObject);
    }
}
