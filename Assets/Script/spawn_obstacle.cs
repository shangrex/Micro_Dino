using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_obstacle : MonoBehaviour
{
    public Transform obstacle;
    int temp;
    //int block_end = 0;
    // Start is called before the first frame update
    void Start()
    {
        //int temp;
        float height = 0;
        for (int i = 0; i < 2; i++)
        {
            Transform block = Instantiate(obstacle);

            block.parent = transform;
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
            block.localPosition = new Vector3(-5 + 12 * i, height, 0);
            block.tag = "obstacle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
