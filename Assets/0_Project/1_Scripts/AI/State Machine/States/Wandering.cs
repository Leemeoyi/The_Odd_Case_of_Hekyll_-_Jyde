using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : IState
{
    readonly BaseAI baseAI;
    NavMeshAgent agent;
    Animator anim;
    Vector2 destination;
    bool Reached;

    Node currentNode;

    float timer;
    float timerTarget = 0.0f;


    public Wandering(BaseAI baseAI, NavMeshAgent agent, Animator anim)
    {
        this.baseAI = baseAI;
        this.agent = agent;
        this.anim = anim;
    }

    public void OnEnter()
    {
        if (currentNode != null) return;
        
        anim.speed = 1;
        
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = baseAI.transform.position;
        Reached = true;

        foreach (var item in baseAI.NodeManager.Nodes)
        {
            float dist = Vector3.Distance(item.transform.position, currentPos);

            if (dist < minDist)
            {
                tMin = item.transform;
                minDist = dist;
                currentNode = item;
            }
        }
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        if (baseAI.playerCore.playerAction)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;;
            return;
        }
        
        if (Reached == true)
        {
            timer += Time.deltaTime;
            anim.SetBool("IsWalking", false);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;;
            if (timer > timerTarget)
            {
                int rand = Random.Range(0, currentNode.ConnectedNode.Count);
                currentNode = currentNode.ConnectedNode[rand];
                destination = currentNode.transform.position;
                destination += Random.insideUnitCircle * currentNode.NodeRadius;

                Reached = false;
            }
 
        }
        else
        {
            anim.SetBool("IsWalking", true);
            agent.SetDestination(destination);
            agent.isStopped = false;
            if (Vector2.Distance(baseAI.transform.position, destination) < 0.3f)
            {
                Reached = true;
                timer = 0.0f;
                timerTarget = Random.Range(baseAI.MinStandingDuration, baseAI.MaxStandingDuration);
            }
        }
    }


}
