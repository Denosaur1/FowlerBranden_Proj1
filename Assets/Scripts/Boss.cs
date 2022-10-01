//using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Health))]
public class Boss : MonoBehaviour
{
  
    [Header("Boss Stats")]
    [SerializeField] int maxHealth = 10;
    [SerializeField] float speed = 1f;
    [SerializeField] float chargeSpeed = 1f;
    [SerializeField] float actionTime = 10f;
    [SerializeField] float actionTimer = 0f;
    
    
    [Header("Initial additives")]
    [SerializeField] Animator Animator;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathSound;
    [SerializeField] GameObject Jaw = null;
    [SerializeField] Transform target = null;
    [SerializeField] GameObject targetImage = null;
    //hidden variables
    //set through code
    public GameObject curTarget;
    NavMeshAgent agent;
    Health health = null;
    Vector3 chargeTarget;
    string BossState = "Move"; //current stae
    float curSpeed = 1f;
    int curHealth;
    bool Enrage = false;
    bool playerHit;
    bool choiceChosen;
    Collider bossCol;

    //Action List and weights
    List<MoveData> InitialAction;
    List<MoveData> CloseAttack;
    List<MoveData> FarAttack;

    //Initial Action
    [Header("Moving Settings")]
    [SerializeField] int MovingWeight = 1;
    [SerializeField] float MovingTime = 1;
    MoveData Move;
    [Header("Attacking Settings")]
    [SerializeField] int AttackingWeight = 1;
    [SerializeField] float AttackingTime = 1;
    MoveData Attack;

    //Close Attacks
    [Header("Swiping Settings")]
    [SerializeField] int SwipingWeight = 1;
    [SerializeField] float SwipingTime = 1;
    MoveData Swipe;
    [Header("Slam Settings")]
    [SerializeField] int SlamWeightClose = 1;
    [SerializeField] int SlamWeightFar = 1;
    [SerializeField] float SlamTime = 1;
    MoveData Slam;

    //Far Attacks
    [Header("Charging Settings")]
    [SerializeField] int ChargingWeight = 1;
    [SerializeField] float ChargingTime = 1;
    MoveData Charge;
    [Header("Eat Settings (TODO)")]
    [SerializeField] int EatWeight = 1;
    [SerializeField] float EatTime = 1;
    MoveData Eat;
    public struct MoveData {
        public string moveName;
        public float timer;
    
    
    }

    public List<GameObject> MinionList;
    [SerializeField] GameObject minion;
    [SerializeField] float MinionMax;
    GameObject currentMinion;
    //initialize boss
    private void Awake()
    {
        SetupActions();
        bossCol = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        MinionList = new List<GameObject>();
        curSpeed = speed;
        agent.speed = curSpeed;
        health = GetComponent<Health>();
        if (deathParticles != null) { health.deathParticles = deathParticles; }
        if (deathSound != null) { health.deathSound = deathSound; }
        health.maxHealth = maxHealth;
    }

    private void FixedUpdate()
    {

        curHealth = health.curHealth;
        if (!Enrage)
        {
            if (curHealth < (maxHealth / 2))
            {
                ChangeAction("Enrage", -1f);
            }
        }
        
        
        
        ActionState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Colliding with: " +collision.gameObject.name );
        if (BossState == "Charging")
        {
            Debug.Log("Collided while Charging");

            if (collision.gameObject.name == "Player")
            {
                Debug.Log("PLAYER Collided");
                ChangeAction("Idle", 0f);
                if (curTarget != null) { Destroy(curTarget); }

            }
            else if (collision.gameObject.tag == "Wall")
            {
                Debug.Log("Wall Collided");
                ChangeAction("Idle", 0f);
            }


        }
    }



    //Step 1: Choose to attack or continue moving
    void RandomizeAction()
    {
        int choice = UnityEngine.Random.Range(0, InitialAction.Count);
        MoveData choiceMade = InitialAction[choice];
        ChangeAction(choiceMade.moveName, choiceMade.timer);
        chargeTarget = target.transform.position;

        //Debug.Log("Action Taken: " + choiceMade.moveName + " With Choice Value of: " + choice);


    }


    //Step 2: If attack, decide whether to do close or far attack depending on player distance
    void ChooseAttack() {
        if (!choiceChosen)
        {
            
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
                foreach (var hitCollider in hitColliders)
                {
                    //Debug.Log(hitCollider.gameObject.name);
                    if (hitCollider.gameObject.name == "Player")
                    {
                        
                        playerHit = true;
    

                    }
                    
                
                }
            if (playerHit)
            {
               // Debug.Log("Choosing Close");
                int choice = Random.Range(0, CloseAttack.Count);
                MoveData choiceMade = CloseAttack[choice];

                ChangeAction(choiceMade.moveName, choiceMade.timer);

            }
            else {
                //Debug.Log("Choosing Far");
                int choice = Random.Range(0, FarAttack.Count);
                MoveData choiceMade = FarAttack[choice];

                ChangeAction(choiceMade.moveName, choiceMade.timer);
            }






        }





    }

    void ChangeAction(string Action, float time)
    {
        choiceChosen = true;
        if (curTarget != null) { Destroy(curTarget); }
        Animator.SetTrigger(Action);
        BossState = Action;
        actionTimer = time;
        playerHit = false;
        choiceChosen = false;
    }
    void SetupActions()
    {
        //initial
        Move.moveName = "Move";
        Attack.moveName = "Attack";
        //close
        Swipe.moveName = "Swipe";
        Slam.moveName = "Slam";
        //far
        Charge.moveName = "Charging";
        Eat.moveName = "Eat";

        //initial
        Move.timer = MovingTime;
        Attack.timer = AttackingTime;

        //close
        Slam.timer = SlamTime;
        Swipe.timer = SwipingTime;

        //far
        Charge.timer = ChargingTime;
        Eat.timer = EatTime;

        InitialAction = new List<MoveData>();
        for (int i = 0; i < MovingWeight; i++)
        {
            InitialAction.Add(Move);
           
        }
        for (int i = 0; i < AttackingWeight; i++)
        {
            InitialAction.Add(Attack);
  
        }


        CloseAttack = new List<MoveData>();
        for (int i = 0; i < SlamWeightClose; i++)
        {
            CloseAttack.Add(Slam);
          
        }
        for (int i = 0; i < SwipingWeight; i++)
        {
            CloseAttack.Add(Swipe);
            
        }


        FarAttack = new List<MoveData>();
        for (int i = 0; i < ChargingWeight; i++)
        {
            FarAttack.Add(Charge);
           
        }
        for (int i = 0; i < SlamWeightFar; i++)
        {
            FarAttack.Add(Slam);

        }




    }

    void ActionState() {
        if (actionTimer >= 0)
        {

            if (actionTimer < actionTime)
            {
                actionTimer += Time.deltaTime;
            }
            else
            {
                //Debug.Log("Attempting to Randomize Action");
                RandomizeAction();
            }
        }
        //transform.LookAt(target);

        //States
        switch (BossState) 
        {
            
            case "Move":
                MoveToPlayer();
                break;
            case "Attack":
                ChooseAttack();
                break;
            case "Idle":
                Idle();
                break;
            case "Slam":
                Slamming();
                break;
            case "Swipe":
                Swiping();
                break;  
            case "Eat":
                Eating();
                break; 
            case "Charging":
                ChargeToPlayer();
                break;
            case "Enrage":
                Enraging();
                break;
        
        
        
        
        }
    
    
    
    
    }

    //Possible Actions

    //move-related
    void MoveToPlayer()
    {

        agent.speed = curSpeed;
        //agent.destination = target.position;
        agent.SetDestination(target.position);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, curSpeed); 
    }
    void ChargeToPlayer()
    {


        //transform.position = Vector3.MoveTowards(transform.position, chargeTarget, chargeSpeed);
        agent.speed = chargeSpeed;
        agent.SetDestination(chargeTarget);
        if (curTarget == null)
        {
            curTarget = Instantiate<GameObject>(targetImage, chargeTarget, Quaternion.identity);
        }
        if (agent.remainingDistance <= 1)
        {

            ChangeAction("Idle", .5f);
            playerHit = false;
            Destroy(curTarget);
        }


    }
    //Idle
    void Idle()
    {
        if (actionTimer >= 1)
        {

            ChangeAction(Move.moveName, Move.timer);

        }

    }
    //attack related
    void Slamming()
    {

        if (!playerHit)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.name == "Player")
                {
                    Debug.Log("PLAYER Slammed");
                    playerHit = true;

                }

            }
        }


        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            int randMinion = Random.Range(1, 3);
            
            SpawnMinons(randMinion);
            ChangeAction("Idle", .5f);

        }


    }
    void Swiping()
    {
        if (!playerHit)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.name == "Player")
                {
                    Debug.Log("PLAYER HIT");
                    playerHit = true;

                }

            }
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {


            ChangeAction("Idle", .7f);
            playerHit = false;
        }

    }

    void Eating() {
  
        //Is he Hungry -> less than half health?
        //yes
        if (curHealth <= maxHealth / 2)
        {
            //Is there food
            //yes
            if (MinionList.Count > 0)
            {
                //need to choose food

                if (!currentMinion)
                {
                    int choosenFood = Random.Range(0, MinionList.Count - 1);
                    for (int b = 0; b < MinionList.Count; b++)
                    {

                        if (MinionList[b].name == choosenFood.ToString())
                        {

                            currentMinion = MinionList[b];
                            agent.SetDestination(currentMinion.transform.position);
                        }

                    }
                    //food not found - reorganize list and give up
                    if (!currentMinion)
                    {
                        for (int b = 0; b < MinionList.Count; b++)
                        {
                            MinionList[b].name = b.ToString();
                        }
                        ChangeAction("Idle", .5f);
                        playerHit = false;
                    }
                }
                //food found
                if (currentMinion){
                        agent.SetDestination(currentMinion.transform.position);
                        if (!curTarget){
                            Debug.Log(currentMinion + " Exists!");
                            curTarget = Instantiate<GameObject>(targetImage, currentMinion.transform.position, Quaternion.identity);
                        } else { curTarget.transform.position = currentMinion.transform.position; }
                        if (agent.remainingDistance <= 1){
                            playerHit = false;
                            Destroy(curTarget);
                            Destroy(currentMinion);
                            MinionList.Remove(currentMinion);
                            Debug.Log(MinionList.Count);
                            health.ChangeHealth(1);
                            curHealth = Mathf.Clamp(curHealth, 0, maxHealth / 2);
                            ChangeAction("Idle", .5f);
                        }
                    }
                    
              
            }
            //no food
            else
            {
                ChangeAction("Idle", .5f);
                playerHit = false;
            }
            
        }
        //not hungry
        else
        {
            ChangeAction("Idle", .5f);
            playerHit = false;
        }


        

    }
    void Enraging() {
        
        if (!Enrage)
        {
            bossCol.enabled = !bossCol.enabled;
            agent.isStopped = true;
            curSpeed = 3 * speed;
            for (int i = 0; i < EatWeight; i++)
            {
                FarAttack.Add(Eat);

            }
            Enrage = true;
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {

            bossCol.enabled = !bossCol.enabled;
            
            playerHit = false;
            Enrage = true;
             if (Jaw != null)
            {
                Destroy(Jaw);

            }
            agent.isStopped = false;
            ChangeAction("Idle", .7f);
        }


    }

    void SpawnMinons(int amount) {

        if (MinionList.Count <= MinionMax)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newMinion = Instantiate(minion, new Vector3(Random.Range(-15, 30), 0.5f, Random.Range(-7, 30)), Quaternion.identity);
              
                MinionList.Add(newMinion);
                for (int b = 0; b < MinionList.Count; b++)
                {
                    MinionList[b].name = b.ToString();

                }
            }
        }

    
    
    }


    

  


}
