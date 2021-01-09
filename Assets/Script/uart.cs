using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class uart : MonoBehaviour
{
    SerialPort serial_port = new SerialPort("COM4", 1200);
    private string port_name;
    private string serial_buffer;
    // Start is called before the first frame update
    void Start()
    {
        serial_buffer = "";
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

    // Update is called once per frame
    void Update()
    {
        try {
            string s = serial_port.ReadExisting();
            if (s == "\n") {
                Debug.Log(serial_buffer);
                serial_buffer = "";
            } else {
                serial_buffer += s;
            }
            //serial_port.BaseStream.Flush();
        } catch (System.Exception e) {
            //Debug.Log(e.Message);
        }
    }

    void OnApplicationQuit()
    {
        serial_port.Dispose();
        Debug.Log("bye");
    }
}
