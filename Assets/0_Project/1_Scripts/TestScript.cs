using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    int speed;
    float sprint;

    GameObject collidedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collidedObject != null)
            {
                Destroy(collidedObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Enter Collision");

        collidedObject = other.gameObject;
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     print("Exit Collision");
    //     collidedObject = null;
    // }
}
