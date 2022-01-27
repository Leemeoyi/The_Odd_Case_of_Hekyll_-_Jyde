using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Police : BaseAI
{

    PlayerCore pc;
    [SerializeField] float chaseSpeed;
    [SerializeField] float outerDetectionRadius = 3.0f;
    [SerializeField] float innerDetectionRadius = 1.5f;
    [SerializeField] float collisionRadius = 1f;
    [SerializeField] float searchingTime;

    public float ChaseSpeed => chaseSpeed;
    public float SearchingTime => searchingTime;

    bool isOverlapped = false;
    public bool IsOverlapped => isOverlapped;

    bool isOnSight = false;
    public bool IsOnSight => isOnSight;

    bool isPursuing = false;
    public bool IsPursing
    {
        get => isPursuing;
        set => isPursuing = value;
    }

    LayerMask mask;

    protected override void Awake()
    {
        base.Awake();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>();
    }

    Vector2 lastPos;
    public Vector2 LastPos
    {
        get => lastPos;
        set => lastPos = value;
    }

    protected override void Start()
    {
        base.Start();
        
        mask = LayerMask.GetMask("Default");
        var wandering = new Wandering(this, agent, anim);
        var chase = new Chase(this, agent, anim, pc);
        var searching = new Searching(this, agent, anim, pc);


        stateMachine.AddAnyTransition(chase, PlayerOnSight());
        //At(wandering, chase, PlayerLoseSight());

        At(searching, chase, PlayerLoseSight());
        At(wandering, searching, ForgetPlayer());

        stateMachine.SetState(wandering);

        void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
        Func<bool> PlayerOnSight() => () => (isOnSight && pc.IsHeckyll);
        Func<bool> PlayerLoseSight() => () => (!isOnSight);
        Func<bool> ForgetPlayer() => () => (!isPursuing );
    }

    private void Update()
    {
        if (prevPos < transform.position.x)
            sr.flipX = false;
        else if (prevPos > transform.position.x)
            sr.flipX = true;
        
        if (Vector2.Distance(pc.transform.position, transform.position) < collisionRadius)
        {
            pc.gameObject.SetActive(false);
            CoreManager.instance.GameOver();
            print("Caught the player");
        }

        stateMachine.Tick();

        prevPos = transform.position.x;
    }

    void FixedUpdate()
    {
        if (isOverlapped)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, pc.transform.position, mask);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player") && pc.IsHeckyll)
                {
                    Debug.DrawLine(this.transform.position, pc.transform.position, Color.red);
                    isOnSight = true;
                }
                else
                {
                    isOnSight = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isOverlapped = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOverlapped = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, outerDetectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = outerDetectionRadius;
    }
}
