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
        Collider[] zombiesInRange = Physics.OverlapSphere(gameObject.transform.position, radius, layerMask);

        foreach(Collider zombie in zombiesInRange)
        {
            zombie.gameObject.GetComponent<ZombieObject>().activate(this.gameObject);
        }

        // Move left/right with <- and -> or 'a' and 'd'
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }

        direction = direction * speed * Time.deltaTime;

        gameObject.transform.position += direction;

        

    }
}