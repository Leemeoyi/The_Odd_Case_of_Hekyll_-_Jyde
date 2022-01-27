using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator animator;

    SpriteRenderer sr;
    Rigidbody2D rb;
    PlayerCore playerCore;
    Vector2 movement;

    void Awake()
    {
        playerCore = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    //! Chris: Changed the movement method to use translate instead to avoidd jittering from the rigidbody body
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (playerCore.IsHeckyll)
        {
            animator.SetBool("IsJyde", false);
        }
        else
        {
            animator.SetBool("IsJyde", true);
        }

        if (movement.x > 0)
        {
            sr.flipX = false;
        }
        else if (movement.x < 0)
        {
            sr.flipX = true;
        }

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        transform.Translate((movement * Time.deltaTime) * speed, Space.Self);
    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
    }
}
