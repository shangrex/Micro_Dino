using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Uart : MonoBehaviour
{
    public SerialPort serial_port;
    public static string port_name = "COM6";
    public static int baud_rate = 9600;
    public string serial_buffer;
    public string data;
    public bool interrupt_flag;

    // Start is called before the first frame update

    public Uart()
    {
        OpenSerial(port_name, baud_rate);
    }

    public void OpenSerial(string port, int baud)
    {
        serial_port = new SerialPort(port, baud);
        serial_buffer = "";
        data = "";
        interrupt_flag = false;
        try {
            serial_port.Open();
            if (!serial_port.IsOpen) {
                Debug.Log("Fail to open " + port);
                return;
            } else {
                Debug.Log("Success to open " + port);
            }
        } catch (System.Exception e) {
            serial_port.Dispose();
            Debug.Log(e.Message);
        }
    }

    public void CloseSerial()
    {
        serial_buffer = "";
        data = "";
        interrupt_flag = false;
        serial_port.Dispose();
        Debug.Log("Close port: " + port_name);
    }

    public void Start()
    {
        OpenSerial(port_name, baud_rate);
    }

    // Update is called once per frame
    public void Update()
    {
        try {
            string s = serial_port.ReadExisting();
            if (s[0] == 'i') {
                data = "";
                Debug.Log("hi");
                interrupt_flag = true;
            } else if (s == "\n") {
                //Debug.Log(serial_buffer);
                data = serial_buffer;
                serial_buffer = "";
            } else {
                serial_buffer += s;
            }
        } catch (System.Exception) {
            //Debug.Log(e.Message);
        }
    }

    public void Send(string s)
    {
        serial_port.Write(s);
    }

    void OnApplicationQuit()
    {
        CloseSerial();
    }

    public void Clear()
    {
        serial_port.DiscardInBuffer();
    }
}
