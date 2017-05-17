using UnityEngine;
using System.Collections;

public class PulseDependentLight : MonoBehaviour
{

    public int MinTorchAngle = 45;
    public int MaxTorchAngle = 180;
    public float MinIntensity = 1f;
    public float IntensityStep = 0.5f;
    public float RateChangeIntensityStep = 0.1f;

    private Light torch;

    // Use this for initialization
    void Start()
    {
        torch = gameObject.GetComponent<Light>();
        MonitorDisplay.OnPulseRate += HandleOnPulseRate;
    }

    private void HandleOnPulseRate(float p)
    {
        if (p <= 0)
        {
            torch.spotAngle = MinTorchAngle;
            torch.intensity = MinIntensity;
        } else if (p >= 1)
        {
            torch.spotAngle = MaxTorchAngle;
            float diff = (p - 1) / RateChangeIntensityStep;
            torch.intensity = 1 + diff * IntensityStep;

        } else
        {
            float diff = MaxTorchAngle - MinTorchAngle;
            torch.spotAngle = MinTorchAngle + Mathf.RoundToInt(p * diff);
            torch.intensity = MinIntensity;
        }
    }
}
