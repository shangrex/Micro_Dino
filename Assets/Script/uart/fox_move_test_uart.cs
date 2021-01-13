using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//[RequireComponent(typeof(Collider))]
public class fox_move_test_uart : MonoBehaviour
{
    public static int count_time = 0;
    private Rigidbody2D _rigidbody;
    public float speed = 5f;
    public float jumpforce = 500;
    public Animator animator;
    public Image[] image = new Image[5];
    public Text DieText;
    int heart_count = 5;
    int last_ser = 250;
    int standard_ser = 0;
    int standard_t = 0;
    int t = 0;
    int squad_time = 4000;
    bool is_squad = false;
    Uart uart;
    private int serial_speed;

    // Start is called before the first frame update
    void Start()
    {
        uart = new Uart();
        uart.Send("fff");         // pic18 enter fox game state
        transform.localScale = new Vector3(13.6f, 13.6f, 1);
        _rigidbody = GetComponent<Rigidbody2D>();
        animator.SetInteger("status", 1);
        for (int i = 0; i < heart_count; i++)
        {
            image[i].enabled = true;
        }
        DieText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        t += 1;
    
        // get uart value
        uart.Update();
        if (uart.interrupt_flag == true) {
            // jump
            if (heart_count <= 0) {
                uart.Send("mmm"); // pic18 exit fox game state
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
                string s = uart.data;
                bool is_t = false;
                for(int i =0; i < s.Length; i++) {
                    if(s[i] == 't') {
                        is_t = true;
                        count_time += 1;
                    }
                }
                if(is_t == false)serial_speed = Int32.Parse(uart.data);
            } catch (Exception) {
                //Debug.Log("Format Exception: " + uart.data);
            }
        }

       // Debug.Log(uart.data);
        /*
         *  serial_speed is the value of acc
         */
        if(Math.Abs( serial_speed - last_ser) < 20 ) {
            standard_ser = serial_speed;
        }
        if(serial_speed - last_ser > 100 && t - standard_t > 100 && t > 100 && is_squad == false) {
            //jump
            if ( _rigidbody.velocity.y == 0) {
                Debug.Log("jmp");
                _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }

            standard_t = t;
        }
        else if((serial_speed - last_ser < -100 && t - standard_t > 100)) {
            Debug.Log("squad");
            is_squad = true;
            squad_time = 0;
        }
        last_ser = serial_speed;
        
        if(is_squad == true) {
            squad_time += 1;
            ////down in the air
            if (Mathf.Abs(_rigidbody.velocity.y) > 0) {
                animator.SetInteger("status", 5);
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
                _rigidbody.AddForce(new Vector2(0, -0.5f), ForceMode2D.Impulse);
            }
            //down on the land
            else {
                animator.SetInteger("status", 2);
                transform.localScale = new Vector3(13.6f, 8.6f, 1);
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
            }

            standard_t = t;
        }

        if(squad_time > 700) {
            is_squad = false;
            transform.localScale = new Vector3(13.6f, 13.6f, 1);

        }

        if (count_time % 10 == 0) {
            //bizzer loudly speek

        }
        if (_rigidbody.velocity.y > 0) {
            animator.SetInteger("status", 3);
        } else if (_rigidbody.velocity.y < 0) {
            animator.SetInteger("status", 5);
        } else {
            animator.SetInteger("status", 1);
        }
        var movement = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(1);
        }

        ////down in the air
        if (Input.GetKey(KeyCode.S) && Mathf.Abs(_rigidbody.velocity.y) > 0) {
            animator.SetInteger("status", 5);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
            _rigidbody.AddForce(new Vector2(0, -0.5f), ForceMode2D.Impulse);
        }
        //down on the land
        else if (Input.GetKey(KeyCode.S)) {
            animator.SetInteger("status", 2);
            transform.localScale = new Vector3(13.6f, 8.6f, 1);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -speed;
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            transform.localScale = new Vector3(13.6f, 13.6f, 1);
        }
        //left and check border
        if (Input.GetKey(KeyCode.A) && transform.position.x > -92) {
            animator.SetInteger("status", 1);
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        }
        ////right
        if (Input.GetKey(KeyCode.D) && transform.position.x < -71.5) {
            animator.SetInteger("status", 1);
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        }
        //jmp
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && _rigidbody.velocity.y == 0) {
            Debug.Log("jmp");
            _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }

        //if (Input.GetKey(KeyCode.S))
        //{
        //    animator.SetInteger("status", 2);
        //    _rigidbody.AddForce(new Vector2(0, -jumpforce / 3), ForceMode2D.Impulse);
        //}


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "obstacle" && heart_count > 0)
        {
            heart_count -= 1;
            image[heart_count].enabled = false;
            collision.transform.position = new Vector3(collision.transform.position.x, 10000, collision.transform.position.z);
        }
        if (heart_count == 0)
        {
            DieText.enabled = true;
            transform.position = new Vector3(99999, 99999, 99999);
        }

    }
    public int get_time()
    {
        return count_time;
    }
    
    private void OnApplicationQuit()
    {
        //uart.Send("ggg"); // pic18 exit fox game state
        uart.CloseSerial();
    }
}
