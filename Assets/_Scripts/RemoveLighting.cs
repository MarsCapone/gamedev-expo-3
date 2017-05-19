using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLighting : MonoBehaviour {

    public float lightLevel = 0.01f;

	// Use this for initialization
	void Start () {

        Light[] allLights = gameObject.GetComponentsInChildren<Light>();
        for (int i = 0; i < allLights.Length; i++)
        {
            Light light = allLights[i];
            light.intensity = lightLevel;
        }
	}
}
