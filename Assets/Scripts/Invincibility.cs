using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
   
    float timer = 0;
    [SerializeField] Material powerUpMat;
    Player iPlayer;
    protected override void PowerUp(Player player, float dur) {
        timer = dur;
        iPlayer = player;


    }
    private void Update()
    {
        if (iPlayer != null)
        {
            if (timer > 0)
            {
                iPlayer.damageProof = true;
                for (int i = 0; i < iPlayer.playerArt.Length; i++)
                {
                    iPlayer.playerArt[i].GetComponent<MeshRenderer>().material = powerUpMat;

                }
                timer--;
            }
            else
            {

                PowerDown(iPlayer);
                iPlayer = null;
            }
        }
    }
    protected override void PowerDown(Player player)
    {

        player.damageProof = false;
        for (int i = 0; i < player.playerArt.Length; i++)
        {
            player.playerArt[i].GetComponent<MeshRenderer>().material = player.playerMat[i];

        }
        gameObject.SetActive(false);
    }
}
