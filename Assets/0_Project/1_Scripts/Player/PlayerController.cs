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
    [HideInInspector] public AnimatorClipInfo[] currentAnim;
    bool isMoving = false;

    void Awake()
    {
        playerCore = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    //! Chris: Changed the movement method to use translate instead to avoidd jittering from the rigidbody body
    void Update()
    {
        currentAnim = animator.GetCurrentAnimatorClipInfo(0);
        if (currentAnim[0].clip.name == "PlayerTransformToJyde" ||
            currentAnim[0].clip.name == "PlayerTransformToHeckell" || currentAnim[0].clip.name == "PrepAttack"
            ||  currentAnim[0].clip.name == "Attack")
        {
            AudioManager.instance.SFX_Source.clip = null;
            isMoving = false;
            playerCore.playerAction = true;
            return;
        }
        else
            playerCore.playerAction = false;

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
            playerCore.isLeft = false;

        }
        else if (movement.x < 0)
        {
            sr.flipX = true;
            playerCore.isLeft = true;
        }

        if (movement.x != 0 || movement.y != 0)
        {
            isMoving = true;
            animator.SetBool("IsWalking", isMoving);
        }
        else
        {
            isMoving = false;
            animator.SetBool("IsWalking", isMoving);
        }

        
        if (isMoving)
        {
            if (!AudioManager.instance.SFX_Source.isPlaying)
                    AudioManager.instance.PlayLoopingSFX(playerCore.audioData, "Footstep");
        }
        else
        {
            if (AudioManager.instance.SFX_Source.clip != null)
            {
                if (AudioManager.instance.SFX_Source.clip.name == "FootstepCombined" &&
                    AudioManager.instance.SFX_Source.isPlaying)
                {
                    //AudioManager.instance.SFX_Source.Stop();
                    AudioManager.instance.SFX_Source.clip = null;
                }
            }
        }

        //transform.Translate((movement.normalized * Time.deltaTime) * speed, Space.Self);
    }

    void FixedUpdate()
    {
        currentAnim = animator.GetCurrentAnimatorClipInfo(0);
        if (currentAnim[0].clip.name == "PlayerTransformToJyde" ||
            currentAnim[0].clip.name == "PlayerTransformToHeckell"
            || currentAnim[0].clip.name == "PrepAttack"
            || currentAnim[0].clip.name == "Attack")
        {
            playerCore.playerAction = true;
            return;
        }
        else
            playerCore.playerAction = false;
        
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        
    }
}
