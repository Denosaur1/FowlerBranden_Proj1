using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Collectible
{
    [SerializeField] int score = 1;
    protected override void Collect(Player player)
    {
        player.IncreaseScore(score);
        

    }

}
