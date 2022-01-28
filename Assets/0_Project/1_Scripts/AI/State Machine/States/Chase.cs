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
    Vector2 randInsideCircle;
    float originalSpeed;
    
    Vector2 targetPos;

    float timer;
    float startChaseTime = 0.75f;

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
        agent.ResetPath();
        policeAI.IsPursing = true;
        policeAI.LastPos = playercore.transform.position;
        randInsideCircle = Random.insideUnitCircle * playercore.radiusSize;
        anim.speed = 1.5f;
        agent.isStopped = false;
        timer = 0;
    }

    public void OnExit()
    {
        agent.speed = originalSpeed;
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        if (timer < startChaseTime)
            return;
        

        if (policeAI.IsOnSight)
        {
            anim.SetBool("IsWalking", true);
            if (Vector2.SqrMagnitude(policeAI.transform.position - playercore.transform.position) > playercore.radiusSize - 1.5f)
            {
                policeAI.LastPos = playercore.transform.position;
            }
            else
            {
                policeAI.LastPos = playercore.transform.position = randInsideCircle;
            }
        }
        
        agent.SetDestination(policeAI.LastPos);
    }
}
