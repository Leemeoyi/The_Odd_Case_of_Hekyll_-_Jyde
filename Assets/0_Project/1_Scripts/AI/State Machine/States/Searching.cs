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
        Debug.Log("searching");
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
        policeAI.LastPos = Vector2.zero;
    }

    public void Tick()
    {
        if (policeAI.IsOnSight)
        {
            policeAI.LastPos = playercore.transform.position;
        }
        
        agent.SetDestination(policeAI.LastPos);
    }
}