/*
 * Credit:
 * http://www.alanzucconi.com/2015/10/07/how-to-integrate-arduino-with-unity/
 */
using System;
using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class HRMonitor {

    private string port;
    private int baudrate;

    private SerialPort stream;
    private bool testing = false;

    public static int READ_TIMEOUT = 50;


    /**
     * Initialise a heart rate monitor.
     * @param port The port where the Arduino is connected. E.g. "COM4".
     * @param baudrate The baudrate for the serial port.
     */
    public HRMonitor(string port, int baudrate) {
        this.port = port;
        this.baudrate = baudrate;
    }

    public HRMonitor(string port) {
        this.port = port;
        baudrate = 115200;
    }

    public void Open() {
        if (!testing)
        {
            stream = new SerialPort(port, baudrate);
            stream.ReadTimeout = READ_TIMEOUT;
            stream.Open();
        }
    }

    public static void SetReadTimeout(int timeout)
    {
        READ_TIMEOUT = timeout;
    }

    public string Read(int timeout = 0) {
        if (!testing)
        {
            stream.ReadTimeout = timeout;
            try
            {
                return stream.ReadLine();
            }
            catch (TimeoutException e)
            {
                return null;
            }
        } else
        {
            return null;
        }
    }

    public void SetTesting(bool t)
    {
        this.testing = t;
    }
}
