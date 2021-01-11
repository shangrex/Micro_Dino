using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_move : MonoBehaviour
{
    private Rigidbody rb;
    public float torque = 100;
    public float maxAngle = 30;
    public float speed;
    public float forwardacc, revertacc, maxspeed, turnstr = 100f, gravity_force = 10f;
    private bool is_ground = true;
    public LayerMask whatisground;
    public Transform groundraypoint;
    public float groundlength = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -.5f, 0);
        rb.transform.parent = null;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rb.transform.position;

        //collision detect
        RaycastHit hit;
        is_ground = false;
        if(Physics.Raycast(groundraypoint.position, -transform.up, out hit, groundlength, whatisground))
        {
            is_ground = true;
        }
        float forward = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float input_speed = 0;
        if(forward > 0)
        {
            input_speed = forwardacc * forward * speed;
        }
        else if(forward < 0)
        {
            input_speed = revertacc * forward * speed;
        }
        if (is_ground)
        {
            if (Mathf.Abs(input_speed) > 0)
            {
                rb.AddForce(transform.forward * input_speed);

            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, steer * turnstr * Time.deltaTime * forward, 0f));

        }
        else
        {
            rb.AddForce(Vector3.up * gravity_force * -1);
        }

    }




}
