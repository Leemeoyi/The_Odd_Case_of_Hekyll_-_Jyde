using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Police : BaseAI
{

    PlayerCore pc;
    [SerializeField] float chaseSpeed;

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
        stateMachine.Tick();

        print(isOverlapped);
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
}
