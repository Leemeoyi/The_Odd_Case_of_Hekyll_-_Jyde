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

    public float ChaseSpeed => chaseSpeed;

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

        var wandering = new Wandering(this, agent, anim);
        var chase = new Chase(this, agent, anim, pc);
        var searching = new Searching(this, agent, anim, pc);
        
        
        stateMachine.AddAnyTransition(chase, PlayerOnSight());
        //At(wandering, chase, PlayerLoseSight());
        
        At(searching, chase, PlayerLoseSight());
        At(wandering, searching, ForgetPlayer());
        
        stateMachine.SetState(wandering);
        
        void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);
        Func<bool> PlayerOnSight() => () => (isOnSight == true);
        Func<bool> PlayerLoseSight() => () => (isOnSight == false);
        Func<bool> ForgetPlayer() => () => (isPursuing == false);

        agent.speed = Speed;
    }

    private void Update()
    {
        if (Vector2.Distance(pc.transform.position, transform.position) < collisionRadius)
        {
            pc.gameObject.SetActive(false);
            print("Caught the player");
        }
        
        stateMachine.Tick();
    }

    void FixedUpdate()
    {
        
        if (isOverlapped)
        {
            LayerMask layerMask = LayerMask.GetMask("Default");
            Debug.DrawLine(this.transform.position, pc.transform.position, Color.red);
            RaycastHit2D hit = Physics2D.Linecast(transform.position, pc.transform.position, layerMask);

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                isOnSight = true;
            }
            else
            {
                isOnSight = false;
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
