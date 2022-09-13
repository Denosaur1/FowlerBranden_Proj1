using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : Collectible
{
    [SerializeField] int healthAdded = 1;
    protected override void Collect(Player player) {

        player.IncreaseHealth(healthAdded);
    
    }
    
}
