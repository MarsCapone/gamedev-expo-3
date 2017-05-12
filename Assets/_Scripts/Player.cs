using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}
