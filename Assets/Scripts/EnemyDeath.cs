using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : DeathEffect
{
    Boss boss;
    private void Awake()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }
    public override void Death()
    {
        boss.MinionList.Remove(this.gameObject);
        if (boss.curTarget != null) { Destroy(boss.curTarget); }
        Destroy(this.gameObject);
    }
}
