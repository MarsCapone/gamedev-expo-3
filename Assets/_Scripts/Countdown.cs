using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public delegate void CalibrationComplete();
    public static event CalibrationComplete OnCalibrationComplete;

    public MonitorDisplay monitor;

    [Tooltip("Set as a negative value in order to use the length of the audio clip.")]
    public float countdownTime = 90f;
    public bool clipLengthCountdown;

    private List<int> rates;
    private Text countdownText;
    private AudioSource audioSource;
    private bool start = false;
    private float startStartTime;
    private float initCountdownTime;

    void Start () {
        rates = new List<int>();
        countdownText = gameObject.GetComponent<Text>();
        countdownText.text = countdownTime.ToString("F1");
        initCountdownTime = countdownTime;

        audioSource = gameObject.GetComponent<AudioSource>();
        if (clipLengthCountdown)
        {
            countdownTime = audioSource.clip.length;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (start)
        {
            rates.Add(monitor.GetRate());
            countdownTime = initCountdownTime - (Time.time - startStartTime);
        }

        if (countdownTime <= 0)
        {
            start = false;
            // average out the rates to get the base rate
            float total = 0f;
            foreach (int r in rates)
            {
                total += r;
            }

            if (monitor.testing)
            {
                monitor.SetBaseRate(70);
                monitor.SetInstaDeathRate(200);
                monitor.Rate = 70;
            }
            else monitor.SetBaseRate(Mathf.RoundToInt(total / rates.Count));

            audioSource.Stop();

            // now load the next bit
            Canvas calibrationCanvas = gameObject.GetComponentInParent<Canvas>();
            calibrationCanvas.gameObject.SetActive(false);

            if (OnCalibrationComplete != null) OnCalibrationComplete();
        }
        countdownText.text = countdownTime.ToString("F1");
	}

    public void StartCountdown()
    {
        if (monitor.testing)
        {
            countdownTime = -1;
        }
        else
        {
            start = true;
            startStartTime = Time.time;
            audioSource.Play();
        }
    }
}
