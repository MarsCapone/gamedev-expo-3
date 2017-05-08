using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObject : MonoBehaviour {

    //settings
    public float fractionMove = 0.7f;

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



    // Use this for initialization
    void Start () {
        speed = startSpeed;
        home = gameObject.transform.position;
        if(Random.value < fractionMove)
        {
            roaming = true;
            setNewRoamDest();
        }
        else
        {
            standing = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (alerted)
        {
            currentDest = player.transform.position;
        }
        if (roaming || alerted || closing)
        {
            Vector3 direction = currentDest - transform.position;
            Vector3 movement = direction.normalized * speed * Time.deltaTime;
            if (movement.magnitude > direction.magnitude)
            {
                movement = direction;
            }

            // move the character:
            GetComponent<CharacterController>().Move(movement);
            
            //TODO: Scale with final models
            if((gameObject.transform.position - currentDest).magnitude < 1.1 )
            {
                if (roaming)
                {
                    setNewRoamDest();
                }else if (alerted)
                {
                    Debug.Log("YOU DIED!");
                }
            }
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
            alerted = true;

            currentDest = player.transform.position;

            speed = maxSpeed;

        }
    }
}