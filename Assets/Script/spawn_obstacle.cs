using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    float height;
    float speed = 5f;
    public Transform obstacle;
    public Transform[] block = new Transform[2];
    int temp;
    //int block_end = 0;
    // Start is called before the first frame update
    void Start()
    {
        int temp;
        float height = 0;
        for (int i = 0; i < 2; i++)
        {
            block[i] = Instantiate(obstacle);

            block[i].parent = transform;
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
            temp = Random.Range(0, 3);

            //block.localPosition = new Vector3(-28, height, 0);
            block[i].localPosition = new Vector3(-5 + 12 * i, height, 0);
            block[i].tag = "obstacle";
            block[i].name = i.ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        int count_time = fox_move_test.count_time;
        //when % 100 speed += 1
        if(count_time % 10 == 0 && speed < 10f && count_time != 0)
        {
            speed += 0.5f;
            Debug.Log(count_time);
            Debug.Log(speed);
        }
        int temp;
        for (int i = 0; i < 2; i++) {
            block[i].localPosition += speed * Time.deltaTime * Vector3.left;
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
            if (block[i].localPosition.x < -29)
            {
                block[i].localPosition = new Vector3(-5, height, 0);
            }
        }
    }

}
