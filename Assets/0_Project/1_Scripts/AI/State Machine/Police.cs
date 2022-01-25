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

    public float ChaseSpeed => chaseSpeed;

    bool isOverlapped = false;
    public bool IsOverlapped => isOverlapped;
    
    bool isOnSight = false;
    public bool IsOnSight => isOnSight;

    protected override void Awake()
    {
        base.Awake();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCore>();

    }

    protected override void Start()
    {
        base.Start();

        var wandering = new Wandering(this, agent, anim);
        var chase = new Chase(this, agent, anim, pc);
        
        At(chase, wandering, PlayerOnSight());
        At(wandering, chase, PlayerLoseSight());
        
        stateMachine.SetState(wandering);
        
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> PlayerOnSight() => () => (isOverlapped == true);
        Func<bool> PlayerLoseSight() => () => (isOverlapped == false);

        agent.speed = Speed;
    }

    private void Update()
    {
        if (Vector2.Distance(pc.transform.position, transform.position) < innerDetectionRadius)
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
                print("HIT!");
            }
            else
            {
                isOnSight = false;
                print(hit.collider.gameObject.name);

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
        
    }

    void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = outerDetectionRadius;
    }
}
