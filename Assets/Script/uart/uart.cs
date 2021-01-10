using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class uart : MonoBehaviour
{
    public static SerialPort serial_port;
    public static string port_name;
    public static string serial_buffer;
    public static string data;
    public static bool interrupt_flag;

    // Start is called before the first frame update

    void OpenSerial(string port, int baud)
    {
        serial_port = new SerialPort(port, baud);
        serial_buffer = "";
        data = "";
        interrupt_flag = false;
        try {
            serial_port.Open();
            if (!serial_port.IsOpen) {
                Debug.Log("Fail to open " + port_name);
                return;
            } else {
                Debug.Log("Success to open " + port_name);
            }
        } catch (System.Exception e) {
            serial_port.Dispose();
            Debug.Log(e.Message);
        }
    }

    void CloseSerial()
    {
        serial_buffer = "";
        data = "";
        interrupt_flag = false;
        serial_port.Dispose();
    }

    void Start()
    {
        OpenSerial("COM4", 9600);
    }

    // Update is called once per frame
    void Update()
    {
        try {
            string s = serial_port.ReadExisting();
            if (s == "\n") {
                //Debug.Log(serial_buffer);
                if (serial_buffer == "i") {
                    data = "";
                    interrupt_flag = true;
                } else {
                    data = serial_buffer;
                }
                serial_buffer = "";
            } else {
                serial_buffer += s;
            }
        } catch (System.Exception) {
            //Debug.Log(e.Message);
        }
    }

    void OnApplicationQuit()
    {
        serial_port.Dispose();
        Debug.Log("bye");
    }
}
