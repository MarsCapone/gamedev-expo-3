using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public int Health = 1000;
    public int MaxHealth = 1000;
    public float RaycastDistance = 5f;
    public float RayThickness = 1f;

    public int HealthIncrease = 1;
    public float HealthIncreaseTime = 1;

    public delegate void DeadNow();
    public static event DeadNow OnDeadNow;

    public delegate void HealthUpdate(int health);
    public static event HealthUpdate OnHealthUpdate;

    public delegate void ReachedGoal();
    public static event ReachedGoal OnReachedGoal;

    private float pulseRatePercentage;

    void Awake()
    {
        MonitorDisplay.OnPulseRate += MonitorDisplay_OnPulseRate;
    }

    void Start()
    {
        if (OnHealthUpdate != null)
        {
            OnHealthUpdate(Health);
        }

        InvokeRepeating("IncreaseHealth", 2f, HealthIncreaseTime);
    }

    private void MonitorDisplay_OnPulseRate(float percentage)
    {
        pulseRatePercentage = percentage;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.SphereCast(transform.position, RayThickness, fwd, out hit, RaycastDistance))
        {
            if (hit.collider.tag == "Zombie")
            {
                Zombie zombie = hit.collider.gameObject.GetComponent<Zombie>();

                zombie.Activate(gameObject);
            } else if (hit.collider.tag == "Finish")
            {
                print("Seen goal");
                if (OnReachedGoal != null)
                    OnReachedGoal();
            }
        }
    }

    void Update()
    {
        if (OnHealthUpdate != null) OnHealthUpdate(Health);
        if (Health < 0)
        {
            // ur dead
            Die();
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Health--;
        }
    }

    private void Die()
    {
        print("Ur dead now!!!");
        if (OnDeadNow != null)
            OnDeadNow();
    }

    void IncreaseHealth()
    {
        if (Health < MaxHealth && pulseRatePercentage < 1)
        {
            Health+=HealthIncrease;
        } 
    }


}
