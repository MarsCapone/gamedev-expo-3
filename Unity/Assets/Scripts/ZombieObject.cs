using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObject : MonoBehaviour {

    //properties

    //the player
    GameObject player;

    //boolean states for zombie to be in
    private bool standing = false;
    private bool roaming = false;
    private bool closing = false;
    private bool alerted = false;

    //roaming variables
    Vector3 home;
    float roamRange = 20f;
    Vector3 currentRoamDest;

    //movmenet speed of zombies
    float speed = 5f;



	// Use this for initialization
	void Start () {
        home = gameObject.transform.position;
        if(Random.value > 0.1f)
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
        if (roaming)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentRoamDest, speed * Time.deltaTime);
            if(gameObject.transform.position == currentRoamDest)
            {
                setNewRoamDest();
            }
        }
	}

    private void setNewRoamDest()
    {
        Vector2 tempRandomDest = Random.insideUnitCircle * roamRange;
        currentRoamDest = home + new Vector3(tempRandomDest.x, 0, tempRandomDest.y);
    }
}
