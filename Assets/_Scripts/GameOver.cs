using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        Player.OnDeadNow += Player_OnDeadNow;
        gameObject.SetActive(false);
    }

    private void Player_OnDeadNow()
    {
        gameObject.SetActive(true);
    }
}
