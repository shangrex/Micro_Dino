using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_obstacle : MonoBehaviour
{
    public Transform obstacle;
    int block_end = 0;
    // Start is called before the first frame update
    void Start()
    {
        int temp;
        float height = 0;
        for (int i = 0; i < 3; i++)
        {
            Transform block = Instantiate(obstacle);

            block.parent = transform;
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
            temp = Random.Range(block_end, 24);
            Debug.Log(block_end);
            Debug.Log(temp);
            Debug.Log("\n\n");
            block.position = new Vector3(13 + block_end + temp, height, 0);
            block_end += temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
