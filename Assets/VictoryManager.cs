using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] Boss bossObject;
    [SerializeField] GunController playerObject;  
    [SerializeField] GameObject victoryText;
    [SerializeField] GameObject lossText;

    private void Awake()
    {
        
        if (victoryText) { victoryText.SetActive(false); }
        if (lossText) { lossText.SetActive(false); }


    }

    void Update()
    {
        if (!bossObject) { Victory(); }
        if (!playerObject) { Loss(); }
    }

    public void Victory() {
        playerObject.enabled = false;
        if (victoryText) { victoryText.SetActive(true); }


    }
    public void Loss() {
        bossObject.enabled =false;
        if (lossText) { lossText.SetActive(true); }
    }


}
