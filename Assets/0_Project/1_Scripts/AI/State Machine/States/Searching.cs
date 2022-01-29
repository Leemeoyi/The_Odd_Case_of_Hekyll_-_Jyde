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
    TowniesManager tm;

    float originalSpeed;

    float timer;
    

    public Searching(Police policeAI, NavMeshAgent agent, Animator anim, PlayerCore playercore, TowniesManager tm)
    {
        this.policeAI = policeAI;
        this.agent = agent;
        this.anim = anim;
        this.playercore = playercore;
        this.tm = tm;
        originalSpeed = policeAI.Speed;
    }
    
    public void OnEnter()
    {
        agent.speed = policeAI.ChaseSpeed;
        policeAI.LastPos = playercore.transform.position;
        timer = policeAI.SearchingTime;
        anim.SetBool("IsSearching", true);
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
        policeAI.LastPos = Vector2.zero;
        tm.RemovePursuiter(policeAI);
    }

    public void Tick()
    {
        timer -= Time.deltaTime;

        if (!agent.hasPath)
            anim.SetBool("IsChasing", false);
        
        if (timer <= 0.0f)
        {
            anim.SetBool("IsSearching", false);

            policeAI.IsPursing = false;
        }
    }
}