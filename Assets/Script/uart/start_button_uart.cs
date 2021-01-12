using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class start_button_uart : MonoBehaviour
{
    public int selectValue;
    public GameObject[] button;
    Uart uart;

    // Start is called before the first frame update
    void Start()
    {
        uart = new Uart();
        selectValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // get uart value
        uart.Update();
        if (uart.interrupt_flag == true) {
            uart.CloseSerial();
            if (selectValue > 512) {
                SceneManager.LoadScene(0);
            } else {
                SceneManager.LoadScene(2);
            }
        } else if (uart.data != "") {
            try {
                selectValue = Int32.Parse(uart.data.Split(',')[0]);
            } catch (Exception) {}
        }

        if (selectValue < 512) {
            button[0].GetComponent<Image>().color = new Color(0.7f, 0.4f, 0.4f);
            button[1].GetComponent<Image>().color = Color.white;
        } else {
            button[0].GetComponent<Image>().color = Color.white;
            button[1].GetComponent<Image>().color = new Color(0.7f, 0.4f, 0.4f);
        }
    }

    void OnApplicationQuit()
    {
        uart.CloseSerial();
    }
}
