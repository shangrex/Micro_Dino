using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class obstacle : MonoBehaviour
{

    public Transform[] fire = new Transform[5];
    public float speed = 0;
    Transform[] ob = new Transform[5];
    public float force = -5;
    private Rigidbody2D[] _rigidbody = new Rigidbody2D[5];
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 5; i++)
        {
            //camera bound in 12 ~ -12
            ob[i] = Instantiate(fire[i]);
            ob[i].parent = transform;
            ob[i].position = new Vector3(13 + Random.Range(0, 30), 0.1f, 0);
            _rigidbody[i] = GetComponent<Rigidbody2D>();
            //_rigidbody[i].velocity = new Vector3(1, 0, 0) * -speed;

        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            //garbaage collection
            if (ob[i].position.x < -12)
            {
                float height = Random.Range(0.0f, 1.0f);
                ob[i].position = new Vector3(13+Random.Range(0, 15), height, 0);
            }
            // ob.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            //_rigidbody[i].AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
            //_rigidbody[i].MovePosition(transform.position + new Vector3(-speed, 0, 0));
            _rigidbody[i].transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

    }
}
