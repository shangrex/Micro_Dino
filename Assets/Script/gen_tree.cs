using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gen_tree : MonoBehaviour
{
    public Transform tree;
    // Start is called before the first frame update
    void Start()
    {
        Transform c = Instantiate(tree);
        c.parent = transform;
        c.position = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
