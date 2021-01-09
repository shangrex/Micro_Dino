using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_move : MonoBehaviour
{
    // Start is called before the first frame update
    int temp;
    float height;
    float speed = 8f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += speed * Time.deltaTime * Vector3.left;
        temp = Random.Range(0, 3);
        switch (temp)
        {
            case 0:
                height = -2.4f;
                break;
            case 1:
                height = 0.4f;
                break;
            case 2:
                height = 2.4f;
                break;
        }
        if (transform.position.x < -12)
            transform.position = new Vector3(13, height, 0);
    }
}
