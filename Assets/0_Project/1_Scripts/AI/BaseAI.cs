using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CivilianTypes
{
    NONE = -1,
    TOWNFOLKS,
    POLICE
}

public class BaseAI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {


    }
    
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
