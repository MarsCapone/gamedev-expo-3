using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public int Health = 10;
    public float RaycastDistance = 5f;

    public delegate void DeadNow();
    public static event DeadNow OnDeadNow;

    public delegate void HealthUpdate(int health);
    public static event HealthUpdate OnHealthUpdate;

    private float pulseRatePercentage;

    void Awake()
    {
        MonitorDisplay.OnPulseRate += MonitorDisplay_OnPulseRate;
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

        if (Physics.Raycast(transform.position, fwd, out hit, RaycastDistance))
        {
            if (hit.collider.tag == "Zombie")
            {
                Zombie zombie = hit.collider.gameObject.GetComponent<Zombie>();

                zombie.Activate(gameObject);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            // being attacked
            Health--;
        }
    }

    private void Die()
    {
        print("Ur dead now!!!");
        if (OnDeadNow != null)
            OnDeadNow();
    }


}
