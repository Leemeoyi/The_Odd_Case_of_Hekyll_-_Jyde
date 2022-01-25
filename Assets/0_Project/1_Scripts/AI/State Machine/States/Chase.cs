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

    public Chase(Police baseAI, NavMeshAgent agent, Animator anim, PlayerCore playercore)
    {
        this.policeAI = baseAI;
        this.agent = agent;
        this.anim = anim;
        this.playercore = playercore;
        originalSpeed = baseAI.Speed;
    }
    
    public void OnEnter()
    {
        Debug.Log("Chasing");
        agent.speed = policeAI.ChaseSpeed;
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
    }

    public void Tick()
    {
        agent.SetDestination(playercore.transform.position);
    }
}
