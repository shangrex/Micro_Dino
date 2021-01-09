using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino_move : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float speed = 5f;
    public float jumpforce = 500;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // && Mathf.Abs(_rigidbody.velocity.y) < 0.001f
        ////up
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += new Vector3(0, movement, 0) * Time.deltaTime * speed;
        //}
        ////down
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position += new Vector3(0, movement, 0) * Time.deltaTime * -speed;
        //}
        ////left
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * -speed;
        //}
        ////right
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;
        //}

        //if(Input.GetKey(KeyCode.Space))
        //{
        //    _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse) ;
        //}

    }
}
