using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    readonly Police policeAI;
    NavMeshAgent agent;
    Animator anim;
    PlayerCore playercore;

    float originalSpeed;
    
    Vector2 targetPos;

    public Chase(Police policeAI, NavMeshAgent agent, Animator anim, PlayerCore playercore)
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
        policeAI.IsPursing = true;
        policeAI.LastPos = playercore.transform.position;
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
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
