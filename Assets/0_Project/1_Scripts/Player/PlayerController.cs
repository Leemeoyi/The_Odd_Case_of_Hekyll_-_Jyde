using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class PlayerController : MonoBehaviour
{
    //add rigidbody2d component to the player

    public float speed;

    public Rigidbody2D rb;
    PlayerCore playerCore;
    Vector2 movement;

    void Awake()
    {
        playerCore = GetComponent<PlayerCore>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
