using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class PlayerController : MonoBehaviour
{
    //add rigidbody2d component to the player

    public float speed;

    Rigidbody2D rb;
    PlayerCore playerCore;
    Vector2 movement;

    void Awake()
    {
        playerCore = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    //! Chris: Changed the movement method to use translate instead to avoidd jittering from the rigidbody body
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        transform.Translate((movement * Time.deltaTime) * speed, Space.Self);
    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
