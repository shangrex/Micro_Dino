using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_move : MonoBehaviour
{
    // Start is called before the first frame update
    int temp;
    float height;
    float speed = 5f;

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
                height = 0f;
                break;
            case 1:
                height = 2.5f;
                break;
            case 2:
                height = 5f;
                break;
        }
        if (transform.localPosition.x < -29)
        {
            transform.localPosition = new Vector3(-5, height, 0);
        }
    }
}
