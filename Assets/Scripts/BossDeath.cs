using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : DeathEffect
{
    Boss boss;
    private void Awake()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }
    public override void Death()
    {
        
        if (boss.curTarget != null) { Destroy(boss.curTarget); }
        Destroy(this.gameObject);
    }
}
