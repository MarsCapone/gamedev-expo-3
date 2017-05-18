using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour {

    public GameObject toggleableObject;
    public KeyCode toggleButton = KeyCode.Escape;
    public bool showAfterCalibration = true;

    private bool showing = false;

    void Awake()
    {
        Countdown.OnCalibrationComplete += Countdown_OnCalibrationComplete;
        MonitorDisplay.OnAbsoluteMax += MonitorDisplay_OnAbsoluteMax;
    }

    private void MonitorDisplay_OnAbsoluteMax(bool b)
    {
        if (b) Show();
    }

    private void Countdown_OnCalibrationComplete()
    {
        Show();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(toggleButton))
        {
            showing = !showing;
        }

        if (showing)
        {
            Time.timeScale = 0; // pause 
        } else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        toggleableObject.SetActive(showing);
	}

    public void SetShowing(bool b)
    {
        showing = b;
    }

    public void Show()
    {
        showing = true;
    }

    public void Hide()
    {
        showing = false;
    }
}
