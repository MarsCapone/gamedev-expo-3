using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int Health = 10;

    public delegate void HealthUpdate(int health);
    public static event HealthUpdate OnHealthUpdate;

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
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
        // some other stuff
    }
}
