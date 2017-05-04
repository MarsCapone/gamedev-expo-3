/*
 * Credit:
 * http://www.alanzucconi.com/2015/10/07/how-to-integrate-arduino-with-unity/
 */
using System;
using System.Collections;
using System.IO.Ports;


public class HRMonitor {

    private string port;
    private int baudrate;

    private SerialPort stream;

    public const int READ_TIMEOUT = 50;

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
        this(port, 9600);
    }

    public void Open() {
        stream = new SerialPort(port, baudrate);
        stream.ReadTimeout = READ_TIMEOUT;
        stream.Open();
    }

    public void Send(string message) {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    public string Read(int timeout = 0) {
        stream.ReadTimeout = timeout;
        try {
            return stream.ReadLine();
        } catch (TimeoutException e) {
            return null;
        }
    }

    /**
     * Invoke with:
     * StartCoroutine(
     *      AsyncronousRead(
     *          (string s) => stringHandler,
     *          () => errorHandler,
     *          10f
     *      );
     * )
     */
    public IEnumerator AsynchronousRead(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity) {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string data = null;

        do {
            try {
                data = stream.ReadLine();
            } catch (TimeoutException) {
                data = null;
            }

            if (data != null) {
                callback(data);
                yield return null;
            } else {
                yield return new WaitForSeconds(0.05f);
            }

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;
        } while (diff.Milliseconds < timeout);

        if (fail != null) {
            fail();
        }

        yield return null;
    }



}
