using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Searching : IState
{
    readonly Police policeAI;
    NavMeshAgent agent;
    Animator anim;
    PlayerCore playercore;

    float originalSpeed;

    float timer;
    

    public Searching(Police policeAI, NavMeshAgent agent, Animator anim, PlayerCore playercore)
    {
        this.policeAI = policeAI;
        this.agent = agent;
        this.anim = anim;
        this.playercore = playercore;
        originalSpeed = policeAI.Speed;
    }
    
    public void OnEnter()
    {
        
        agent.speed = policeAI.ChaseSpeed;
        policeAI.LastPos = playercore.transform.position;
        timer = policeAI.SearchingTime;
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
        policeAI.LastPos = Vector2.zero;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;

        if (!agent.hasPath)
            anim.SetBool("IsWalking", false);
        
        if (timer <= 0.0f)
        {
            policeAI.IsPursing = false;
        }
    }
}