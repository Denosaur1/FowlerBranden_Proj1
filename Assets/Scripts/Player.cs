using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathSound;
    [SerializeField] ParticleSystem shootParticles;
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject curGun;
    [SerializeField] Transform bulletStart;
    public bool damageProof = false;
    [SerializeField] int maxHealth = 3;

    public int MaxHealth {
        get { return maxHealth; }
    }
    [SerializeField] int score = 0;
    public int Score {
        get => Score;
        set => Score = value;
    }
    int curHealth;
    
    [SerializeField] public GameObject[] playerArt; 
    [SerializeField] public Material[] playerMat;
    
    /*
    [SerializeField] TextMeshProUGUI pHealth;
    [SerializeField] TextMeshProUGUI pScore;
    [SerializeField] TextMeshProUGUI pSpeed;
    */
    private void Awake()
    {
       
        playerMat = new Material[playerArt.Length];
        for (int i = 0; i < playerArt.Length; i++) {
            playerMat[i] = playerArt[i].GetComponent<MeshRenderer>().material;
        
        }


    }

    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            Fire();
        
        }
    }
    public void IncreaseHealth(int amount) {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        Debug.Log("Player Health:" + curHealth);

    }
    public void IncreaseScore(int amount) {
        score += amount;
        
        Debug.Log("Score:" + score);

    }
    public void DecreaseHealth(int amount)
    {
        if (!damageProof) { 
            curHealth -= amount;
            Debug.Log("Player Health:" + curHealth);
            if (curHealth <= 0)
            {
                Kill();

            }
        }
    }
    public void Kill() {
        gameObject.SetActive(false);
        //TODO sound and particles
        if (deathParticles != null)
        {
            deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        if (deathSound != null)
        {
            AudioHelper.PlayClip2D(deathSound, 1f);
        }
    }
    public void Fire()
    {
        if (deathParticles != null)
        {
            deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        if (deathSound != null)
        {
            AudioHelper.PlayClip2D(deathSound, 1f);
        }
        if (curGun != null) {
            Instantiate(curGun, bulletStart);
        }
    }

}


