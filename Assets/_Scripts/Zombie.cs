using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    public int MinHitsToDeath = 1;
    public int MaxHitsToDeath = 3;

    public AudioClip AttackSound;
    public AudioClip DeadSound;

    private int hitsToDeath;
    private int currentHits = 0;
    private AudioSource audioSource;
    private bool attackOccurring;

    // -----------------------------

    public float Speed = 1f;
    public float SpeedIncrease = 0.01f;
    public float MaxSpeed = 3f;
    public float MoveChanceMultiplier = 1f;
    public float MoveChanceAdder = 0.005f;

    private float moveChance;
    private float speed;
    private float moveChanceMultiplier;
    private Vector3 homeLocation;
    private Vector3 currentDest;
    private Vector3 startPosition;
    private float ratePercentage;
    private NavMeshAgent navAgent;

    private GameObject player;

    // either closing or not
    // when not closing, go home
    public bool alerted = false;
    public bool closing = false;

    void Awake()
    {
        AnimateWeapon.OnAttack += AnimateWeapon_OnAttack;
        MonitorDisplay.OnPulseRate += MonitorDisplay_OnPulseRate;
    }

    void Start()
    {
        homeLocation = transform.position;
        startPosition = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        moveChance = MoveChanceMultiplier;
        speed = Speed;

        hitsToDeath = Random.Range(MinHitsToDeath, MaxHitsToDeath);
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        moveChance = moveChanceMultiplier * ratePercentage;
    }

    void Update()
    {
        //print(navAgent.pathStatus);
        //print(navAgent.pathPending);
        if (alerted && !closing)
        {
            print("Alerted in update");
            // there's a chance nothing will happen
            if (Random.value > moveChance)
            {
                print("Lucky this time.");
                currentDest = startPosition;
                alerted = false;
                closing = false;
                speed = Speed;
                moveChanceMultiplier = MoveChanceMultiplier;
            } else
            {
                print("Closing in.");
                currentDest = player.transform.position;
                alerted = false;
                closing = true;
            }
        }

        if (closing)
        {

            if ((gameObject.transform.position - currentDest).magnitude <= 2)
            {
                // arrived
                currentDest = startPosition;
                closing = false;
                speed = Speed;
                moveChanceMultiplier = MoveChanceMultiplier;
            }
        } else
        {
            currentDest = startPosition;
            speed = Speed;
            moveChanceMultiplier = MoveChanceMultiplier;
        }

        // move the zombie
        navAgent.speed = speed;
        navAgent.SetDestination(currentDest);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon" && attackOccurring)
        {
            // being hit 
            if (ratePercentage < 1)
            {
                // attacks work
                audioSource.clip = AttackSound;
                audioSource.Play();
                currentHits++;

                if (currentHits >= hitsToDeath)
                {
                    // die
                    gameObject.SetActive(false);
                }
            }
        }
    }


    public void Activate(GameObject player)
    {
        this.player = player;
        moveChanceMultiplier += MoveChanceAdder;
        speed += SpeedIncrease;

        if (ratePercentage >= 1)
        {
            // instadeath. definitely activate.
            alerted = false;
            closing = true;
            currentDest = player.transform.position;
        } else if (ratePercentage > 0)
        {
            
            alerted = true;
        } else
        {
            // impossible to be active
            alerted = false;
            closing = false;
            currentDest = startPosition;

            speed = Speed;
            moveChanceMultiplier = MoveChanceMultiplier;
        }
    }
   

   
   
    private void MonitorDisplay_OnPulseRate(float percentage)
    {
        ratePercentage = percentage;
    }

    private void AnimateWeapon_OnAttack(bool b)
    {
        attackOccurring = b;
    }
}
