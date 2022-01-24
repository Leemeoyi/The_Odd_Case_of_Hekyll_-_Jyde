using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : IState
{
    readonly BaseAI baseAI;
    NavMeshAgent agent;
    Animator anim;
    int rand = 0;

    public Wandering(BaseAI baseAI, NavMeshAgent agent, Animator anim)
    {
        this.baseAI = baseAI;
        this.agent = agent;
        this.anim = anim;
    }

    public void OnEnter()
    {
        rand = Random.Range(0, baseAI.NodeManager.Nodes.Count);
        Debug.Log(rand);
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        agent.SetDestination(baseAI.NodeManager.Nodes[rand].transform.position);
    }
}
