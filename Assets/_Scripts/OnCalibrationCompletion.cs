using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCalibrationCompletion : MonoBehaviour {

    public bool showInitially = true;
    public bool showAfter = false;

    void Awake()
    {
        Countdown.OnCalibrationComplete += HandleOnCalibrationComplete;
        gameObject.SetActive(showInitially);
    }

    void HandleOnCalibrationComplete()
    {
        gameObject.SetActive(showAfter);
    }
}
