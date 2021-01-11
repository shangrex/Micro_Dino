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
                serial_speed = 512 - Int32.Parse(uart.data);
            } catch (FormatException) {
                //serial_speed = 0;
                //Debug.Log("Format Exception: " + uart.data);
            }
        }

        if (_rigidbody.velocity.y > 0) {
            //jump
            animator.SetInteger("status", 3);
        } else if (_rigidbody.velocity.y < 0) {
            //dowm
            animator.SetInteger("status", 5);
        } else {
            //run
            animator.SetInteger("status", 1);
        }
        //down in the air
        if (serial_speed > 0 && Mathf.Abs(_rigidbody.velocity.y) > 0) {
            animator.SetInteger("status", 5);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
        }
        //down on the land
        else if (serial_speed > 0) {
            animator.SetInteger("status", 2);
            transform.localScale = new Vector3(13.6f, 8.6f, 1);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
        } else {
            transform.localScale = new Vector3(13.6f, 13.6f, 1);
        }

        var movement = Input.GetAxis("Horizontal");
        //left
        if (Input.GetKey(KeyCode.A)) {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        }
        ////right
        if (Input.GetKey(KeyCode.D)) {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            transform.localScale = new Vector3(13.6f, 13.6f, 1);
        }
    }
}
