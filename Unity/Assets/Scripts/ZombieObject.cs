using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObject : MonoBehaviour {

    //properties

    //the player
    public GameObject player;

    //boolean states for zombie to be in
    private bool standing = false;
    private bool roaming = false;
    private bool closing = false;
    private bool alerted = false;

    //roaming variables
    Vector3 home;
    float roamRange = 20f;

    //movmenet of zombies
    float speed = 5f;
    Vector3 currentDest;



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

        if (alerted)
        {
            currentDest = player.transform.position;
        }
        if (roaming || alerted || closing)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentDest, speed * Time.deltaTime);
            if(gameObject.transform.position == currentDest)
            {
                if (roaming)
                {
                    setNewRoamDest();
                }else if (alerted)
                {
                    Debug.Log("YOU DEAD!");
                }
            }
        }
	}

    private void setNewRoamDest()
    {
        Vector2 tempRandomDest = Random.insideUnitCircle * roamRange;
        currentDest = home + new Vector3(tempRandomDest.x, 0, tempRandomDest.y);
    }

    public void activate()
    {
        if (!alerted)
        {
            standing = false;
            roaming = false;
            alerted = true;

            currentDest = player.transform.position; 



        }
    }
}