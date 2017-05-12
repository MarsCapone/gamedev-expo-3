using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorDisplay : MonoBehaviour
{

    /*
     * Event for distributing current fraction of rate. Called constantly.
     * @param percentage A the heart rate presented as a fraction of the difference beween base and instadeath.
     *      p >= 1      => Instadeath
     *      p == 0      => Base rate
     *      p < 0       => Less than base rate
     */
    public delegate void PulseRate(float percentage);
    public static event PulseRate OnPulseRate;

    /*
     * Called constantly.
     * @param b Whether the instadeath pulse rate has been reached.
     */
    public delegate void InstaDeath(bool b);
    public static event InstaDeath OnInstaDeath;

    public delegate void BelowBase(bool b);
    public static event BelowBase OnBelowBase;

    public delegate void AbsoluteMax(bool b);
    public static event AbsoluteMax OnAbsoluteMax;
    


    public Color colour = Color.black;
    public Color backgroundColour;
    [Tooltip("Serial port of Arduino, i.e. COM4s")]
    public string port = "COM4";
    public int timeout = HRMonitor.READ_TIMEOUT;
    public bool testing = false;

    public int Rate = 0;

    [Range(15, 100)]
    public int Age = 0;

    [Range(0, 1)]
    [Tooltip("High Difficulty means easy game.")]
    public float Difficulty = 0f;


    private HRMonitor hrm;
    private Text text;
    private Text infoText;
    private Image panel;

    private int diff = 0;
    private int signal = 0;

    private int baseRate = 0;
    private int instaDeathRate = 0;
    private int absoluteMaxRate = 300;

    private void Awake()
    {

        hrm = new HRMonitor(port);
        if (!testing) hrm.Open();
        HRMonitor.SetReadTimeout(timeout);
    }

    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("RateText").GetComponent<Text>();
        text.color = colour;

        Text bpmText = GameObject.Find("BPMText").GetComponent<Text>();
        bpmText.color = colour;

        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.color = colour;

        Image background = gameObject.GetComponent<Image>();

        if (backgroundColour != null)
            background.color = backgroundColour;
    }


    void FixedUpdate()
    {
        string reading;
        if (!testing)
        {
            reading = hrm.Read(10);
            if (reading != null)
            {
                string[] readings = reading.Split(',');
                if (readings.Length == 3)
                {
                    Rate = int.Parse(readings[0]);
                    diff = int.Parse(readings[1]);
                    signal = int.Parse(readings[2]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Rate.ToString();
        infoText.text = GetInfoTextContent();

        if (Age > 0)
            SetInstaDeathRateByAge(Age, Difficulty);
    }

    // handle event updaters
    void LateUpdate()
    {
        if (OnPulseRate != null)
        {
            if (instaDeathRate > 0)
            {
                float percent = (float)(Rate - baseRate) / (instaDeathRate - baseRate);
                OnPulseRate(percent);

                if (OnAbsoluteMax != null)
                {
                    if (Rate == absoluteMaxRate)
                    {
                        OnAbsoluteMax(true);
                    }
                    else
                    {
                        OnAbsoluteMax(false);
                    }
                }

                if (OnInstaDeath != null)
                {
                    OnInstaDeath(percent >= 1f);
                }

                if (OnBelowBase != null)
                {
                    OnBelowBase(percent < 0);
                }

            }
        }
    }

    public int GetRate()
    {
        return Rate;
    }

    public void SetBaseRate(int baseRate)
    {
        this.baseRate = baseRate;
    }

    public float GetDifficulty()
    {
        return Difficulty;
    }

    public int GetAge()
    {
        return Age;
    }

    public int GetBaseRate()
    {
        return baseRate;
    }

    public void SetInstaDeathRate(int instaDeathRate)
    {
        this.instaDeathRate = instaDeathRate;
    }

    public void SetInstaDeathRateByAge(int age, float multiplier)
    {
        this.Age = age;
        absoluteMaxRate = 220 - age;
        instaDeathRate = Mathf.RoundToInt(absoluteMaxRate * multiplier);
    }

    public void SetInstaDeathRateByAge(int age)
    {
        SetInstaDeathRateByAge(age, Difficulty);
    }

    public void SetDifficulty(float difficulty)
    {
        this.Difficulty = difficulty;
    }

    public float GetInstaDeathRate()
    {
        return instaDeathRate;
    }

    public bool IsInstaDeath()
    {
        return Rate > instaDeathRate;
    }

    private string GetInfoTextContent()
    {
        return string.Format(
            "Signal: {0}\n" +
            "Diff: {1}\n" +
            "Base rate: {2}\n" +
            "Insta rate: {3}\n" +
            "Max rate: {4}", signal, diff, baseRate, instaDeathRate, absoluteMaxRate);
    }


    public void DealWithAgeInput(string input)
    {
        if (input.Length > 0)
            this.Age = int.Parse(input);
    }

}