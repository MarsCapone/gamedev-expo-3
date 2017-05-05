using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorDisplay : MonoBehaviour {

    public Color colour = Color.black;
    public Color backgroundColour;
    [Tooltip("Serial port of Arduino, i.e. COM4s")]
    public string port;

    public int hrThreshhold;

    private HRMonitor hrm;
    private Text text;
    private Image panel;

    // Use this for initialization
    void Start () {
        hrm = new HRMonitor(port);
        hrm.SetTesting(true);

        text = GameObject.Find("RateText").GetComponent<Text>();
        panel = gameObject.GetComponent<Image>();
        text.color = colour;

        Text bpmText = GameObject.Find("BPMText").GetComponent<Text>();
        bpmText.color = colour;

        if (backgroundColour != null) 
            panel.color = backgroundColour;
	}
	
	// Update is called once per frame
	void Update () {
        double rate = hrm.GetRate();
        text.text = rate.ToString("0.#");

        int signal = hrm.GetSignal();
        if (signal > hrThreshhold)
        {
            // a heartbeat
        }
	}

    public int GetSignal()
    {
        return hrm.GetSignal();
    }

    public double GetRate()
    {
        return hrm.GetRate();
    }
}
