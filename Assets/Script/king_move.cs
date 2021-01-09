using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class king_move : MonoBehaviour
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

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && _rigidbody.velocity.y == 0)
        {
            Debug.Log("jmp");
            _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(new Vector2(0, -jumpforce/3), ForceMode2D.Impulse);
        }
    }
}
