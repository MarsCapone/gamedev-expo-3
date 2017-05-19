using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Awake () {
        Player.OnHealthUpdate += Player_OnHealthUpdate;
	}

    void Start()
    {
        text = GetComponent<Text>();
    }

    private void Player_OnHealthUpdate(int health)
    {
        text.text = health.ToString();
    }
}
