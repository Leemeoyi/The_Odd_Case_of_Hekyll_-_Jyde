using UnityEngine;
using UnityEngine.AI;

public enum CivilianTypes
{
    NONE = -1,
    TOWNFOLKS,
    POLICE
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class BaseAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator anim;
    [SerializeField] NodeManager nodeManager;
    public NodeManager NodeManager { get => nodeManager; }

    protected StateMachine stateMachine;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        stateMachine = new StateMachine();
    }
}
