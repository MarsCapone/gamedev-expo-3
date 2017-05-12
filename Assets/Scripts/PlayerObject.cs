using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    public float activationRadius = 4;

    public float heartRateMultiplierForClosingIn = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //activate Zombies
        List<Collider> activeZombies = new List<Collider> (getZombiesInRange(activationRadius));
        foreach (Collider zombie in activeZombies)
        {
            zombie.gameObject.GetComponent<ZombieObject>().activate(this.gameObject);
        }

        //close in zombies
        List<Collider>closeZombies = new List<Collider>(getZombiesInRange(activationRadius * heartRateMultiplierForClosingIn));
        closeZombies.Except(activeZombies).ToList();
        foreach (Collider zombie in closeZombies)
        {
            zombie.gameObject.GetComponent<ZombieObject>().Closing(this.gameObject);
        }

        move();

    }

    private Collider[] getZombiesInRange(float range)
    {

        int layerMask = 1 << 8;
        return Physics.OverlapSphere(gameObject.transform.position, range, layerMask);
    }

    private void move()
    {
        float speed = 5f;

        // Move left/right with <- and -> or 'a' and 'd' etc
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }

        direction = direction * speed * Time.deltaTime;

        GetComponent<CharacterController>().Move(direction);
    }
}