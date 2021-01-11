using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fox_move_uart : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed = 5f;
    public float jumpforce = 500;
    public Animator animator;
    private int serial_speed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator.SetInteger("status", 1);
        serial_speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.y > 0) {
            animator.SetInteger("status", 3);
        } else if (_rigidbody.velocity.y < 0) {
            animator.SetInteger("status", 5);
        } else {
            animator.SetInteger("status", 1);
        }

        // get uart value
        if (uart.interrupt_flag == true) {
            // jump
            Debug.Log("jump");
            uart.interrupt_flag = false;
            if (_rigidbody.velocity.y == 0) {
                _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }
        } else if (uart.data != "") {
            try {
                Debug.Log(uart.data);
                serial_speed = 512 - Int32.Parse(uart.data);
            } catch (FormatException) {
                //serial_speed = 0;
                //Debug.Log("Format Exception: " + uart.data);
            }
        }

        // left right
        float ratio = (float)serial_speed / 102.3f;
        transform.position += new Vector3(ratio, 0, 0) * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.S) && Mathf.Abs(_rigidbody.velocity.y) > 0) {
            animator.SetInteger("status", 5);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
        } else if (Input.GetKey(KeyCode.S)) {
            animator.SetInteger("status", 2);
            transform.localScale = new Vector3(13.6f, 8.6f, 1);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
        }

        


        if (Input.GetKeyUp(KeyCode.S)) {
            transform.localScale = new Vector3(13.6f, 13.6f, 1);
        }


    }
}
