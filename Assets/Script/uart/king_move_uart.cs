using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class king_move_uart : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed = 5f;
    public float jumpforce;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        int serial_speed = 0;
        if (uart.interrupt_flag == true) {
            Debug.Log("interrupt");
            uart.interrupt_flag = false;
            if (_rigidbody.velocity.y == 0) {
                _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }
        } else {
            try {
                serial_speed = 512 - Int32.Parse(uart.data);
            } catch (FormatException) {
                serial_speed = 0;
                //Debug.Log("Format Exception: " + uart.data);
            }
        }
        float ratio = (float)serial_speed / 102.3f;
        //Debug.Log(ratio);
        transform.position += new Vector3(ratio, 0, 0) * Time.deltaTime * speed;

    }
}
