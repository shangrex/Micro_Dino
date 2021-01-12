using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fox_move_uart : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed = 5f;
    public float jumpforce = 500;
    public Animator animator;
    private int serial_speed;

    public Image[] image = new Image[5];
    public Text DieText;
    int heart_count = 5;
    Uart uart;
    // Start is called before the first frame update

    void Start()
    {
        uart = new Uart();
        transform.localScale = new Vector3(13.6f, 13.6f, 1);
        _rigidbody = GetComponent<Rigidbody2D>();
        animator.SetInteger("status", 1);
        serial_speed = 0;
        for (int i = 0; i < heart_count; i++) {
            image[i].enabled = true;
        }
        DieText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // get uart value
        uart.Update();
        if (uart.interrupt_flag == true) {
            // jump
            if (heart_count <= 0) {
                uart.CloseSerial();
                SceneManager.LoadScene(1);
            } else {
                uart.interrupt_flag = false;
                if (_rigidbody.velocity.y == 0) {
                    _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                }
            }
        } else if (uart.data != "") {
            try {
                //Debug.Log(uart.data);
                //serial_speed = 512 - Int32.Parse(uart.data);
                serial_speed = 512 - Int32.Parse(uart.data.Split(',')[0]);
            } catch (Exception) {
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

        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "obstacle" && heart_count > 0) {
            heart_count -= 1;
            image[heart_count].enabled = false;
        }
        if (heart_count == 0) {
            DieText.enabled = true;
            transform.position = new Vector3(99999, 99999, 99999);
        }
    }

    private void OnApplicationQuit()
    {
        uart.CloseSerial();
    }
}
