using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    float radius = 4;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        float speed = 5f;
        int layerMask = 1 << 8;
        Collider[] zombiesInRange = Physics.OverlapSphere(gameObject.transform.position, 10, layerMask);

        foreach(Collider zombie in zombiesInRange)
        {
            zombie.gameObject.GetComponent<ZombieObject>().activate();
        }

        // Move left/right with <- and -> or 'a' and 'd'
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        gameObject.transform.position += direction;

        

    }
}