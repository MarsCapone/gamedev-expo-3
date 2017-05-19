using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieObject : MonoBehaviour {

    //settings
    public float fractionMove = 0.7f;
    public float unalertMultiplier = 2;

    //properties

    //the player
    private GameObject player;

    //boolean states for zombie to be in
    private bool standing = false;
    private bool roaming = false;
    private bool closing = false;
    private bool alerted = false;

    //roaming variables
    private Vector3 home;
    public float roamRange = 10f;

    //movmenet of zombies
    public float startSpeed = 1f;
    public float maxSpeed = 5f;
    private float speed;
    private Vector3 currentDest;

    private Vector3 startPos;

    private NavMeshAgent navAgent;

    private float pr;

    void Awake()
    {
        MonitorDisplay.OnPulseRate += MonitorDisplay_OnPulseRate;
    }

    private void MonitorDisplay_OnPulseRate(float percentage)
    {
        pr = percentage;
    }


    // Use this for initialization
    void Start () {

        startPos = this.transform.position;

        speed = startSpeed;
        home = gameObject.transform.position;
        moveOrStay();

        if (roaming)
        {
            setNewRoamDest();
        }

        navAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (alerted)
        {
            if (player.GetComponent<PlayerObject>().activationRadius * unalertMultiplier < (player.transform.position - this.transform.position).magnitude)
            {
                moveOrStay();
                navAgent.SetDestination(startPos);
                currentDest = startPos;
                alerted = false;
            }
            else
            {
                currentDest = player.transform.position;
            }
        }

        if (closing)
        {
            home = (home + player.transform.position) / 2;
        }

        if (roaming || alerted || closing)
        {
            // move the character:
            navAgent.SetDestination(currentDest);
            
            //TODO: Scale with final models
            if((gameObject.transform.position - currentDest).magnitude < 1.1 )
            {
                if (roaming || closing)
                {
                    setNewRoamDest();
                }
                else if (alerted)
                {
                    Debug.Log("YOU DIED!");
                }
            }
        }
	}

    private void moveOrStay()
    {
        if (Random.value < fractionMove)
        {
            roaming = true;
            standing = false;
        }
        else
        {
            roaming = false;
            standing = true;
        }
    }

    private void setNewRoamDest()
    {
        Vector2 tempRandomDest = Random.insideUnitCircle * roamRange;
        currentDest = home + new Vector3(tempRandomDest.x, 0, tempRandomDest.y);
    }

    public void activate(GameObject player)
    {
        this.player = player;
        if (!alerted)
        {
            standing = false;
            roaming = false;
            closing = false;
            alerted = true;

            currentDest = player.transform.position;

            speed = maxSpeed;

        }
    }

    public void Closing(GameObject player)
    {
        if (pr >= 1) DoClosing(player);
        else if (pr > 0)
        {
            if (Random.value < pr * fractionMove)
                DoClosing(player);
            // else lucky escape
        }
        // else also luckyescape
    }

    private void DoClosing(GameObject player)
    {
        this.player = player;
        standing = false;
        roaming = false;
        closing = true;

    }
}