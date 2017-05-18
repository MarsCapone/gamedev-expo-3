using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

    public int MinHitsToDeath = 1;
    public int MaxHitsToDeath = 3;

    public AudioClip AttackSound;
    public AudioClip DeadSound;

    private int hitsToDeath;
    private int currentHits = 0;
    private bool isInstaDeath = false;
    private AudioSource audioSource;

    void Awake()
    {
        MonitorDisplay.OnInstaDeath += HandleOnInstaDeath;
    }

    // Use this for initialization
    void Start () {
        hitsToDeath = Random.Range(MinHitsToDeath, MaxHitsToDeath);
        audioSource = GetComponent<AudioSource>();
	}
	
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            // being hit 
            if (!isInstaDeath)
            {
                // attacks work
                audioSource.clip = AttackSound;
                audioSource.Play();
                currentHits++;

                if (currentHits == hitsToDeath)
                {
                    // die
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void HandleOnInstaDeath(bool b)
    {
        isInstaDeath = b;
    }
}
