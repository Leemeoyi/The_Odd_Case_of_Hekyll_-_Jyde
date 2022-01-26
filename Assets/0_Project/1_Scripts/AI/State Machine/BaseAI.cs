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
    NodeManager nodeManager;
    public NodeManager NodeManager { get => nodeManager; }

    protected StateMachine stateMachine;

    [SerializeField] float speed;
    public float Speed { get => speed; }

    [SerializeField] float maxStandingDuration = 3.0f;
    public float MaxStandingDuration { get => maxStandingDuration; }

    [SerializeField] float minStandingDuration = 1.0f;
    public float MinStandingDuration { get => minStandingDuration; }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        stateMachine = new StateMachine();
    }
    protected virtual void Start()
    {
        nodeManager = NodeManager.instance;
        agent.speed = Speed;
    }
}
