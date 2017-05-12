using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HRBar : MonoBehaviour {

    public MonitorDisplay monitor;
    public Color backgroundColour;
    public Color foregroundColour;
    public Color instaDeathColour;

    private Slider slider;
    private Image background;
    private Image handle;
    private Image foreground;

	void Start () {
        slider = gameObject.GetComponentInChildren<Slider>();
        slider.minValue = -0.3f;
        slider.maxValue = 1.5f;

        Image[] images = slider.GetComponentsInChildren<Image>();
        background = images[0];
        handle = images[2];
        foreground = images[1];

        background.color = backgroundColour;
        foreground.color = foregroundColour;

        MonitorDisplay.OnPulseRate += HandleOnPulseRate;
    }

    void Update()
    {
        // set slider min and max values depending on difficulty level
        slider.maxValue = (float) 1 / monitor.GetDifficulty();
        slider.minValue = -0.3f * slider.maxValue;
    }

    
    // is run every frame and the percentage contains the fraction of the total pulse rate
    void HandleOnPulseRate(float percentage)
    {
        slider.value = percentage;

        if (percentage >= 1f)
        {
            foreground.color = instaDeathColour;
            handle.color = instaDeathColour;
        } else
        {
            foreground.color = foregroundColour;
            handle.color = Color.white;
        }
    }

}
