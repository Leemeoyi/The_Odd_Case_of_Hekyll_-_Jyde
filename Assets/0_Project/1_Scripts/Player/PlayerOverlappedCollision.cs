using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverlappedCollision : MonoBehaviour
{
   [SerializeField] PlayerCore pc;

    void Awake()
    {
        pc = GetComponentInParent<PlayerCore>();
    }
    
    void Update()
    {
        transform.position = pc.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("folk"))
        {
            pc.CollidedObj = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("folk"))
        {
            pc.CollidedObj = null;
        }
    }
}
