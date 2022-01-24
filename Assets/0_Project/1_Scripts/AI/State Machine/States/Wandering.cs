using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : IState
{
    readonly BaseAI baseAI;
    NavMeshAgent agent;
    Animator anim;
    Transform destination;
    bool Reached;

    Node currentNode;

    public Wandering(BaseAI baseAI, NavMeshAgent agent, Animator anim)
    {
        this.baseAI = baseAI;
        this.agent = agent;
        this.anim = anim;
    }

    public void OnEnter()
    {
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
        if (Reached == true)
        {
            int rand = Random.Range(0, currentNode.ConnectedNode.Count);
            currentNode = currentNode.ConnectedNode[rand];
            destination = currentNode.transform;
            Reached = false;
        }
        else
        {
            agent.SetDestination(destination.position);
            if (Vector2.Distance(baseAI.transform.position, destination.position) < 1.0f)
            {
                Reached = true;
            }
        }
    }


}
