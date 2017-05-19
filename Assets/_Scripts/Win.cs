using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    void Awake()
    {
        Player.OnReachedGoal += Player_OnReachedGoal;
        gameObject.SetActive(false);
    }

    private void Player_OnReachedGoal()
    {
        gameObject.SetActive(true);
    }
}
