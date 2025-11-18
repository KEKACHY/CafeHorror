using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [HideInInspector] public StateMachine StateMachine;
    [HideInInspector] public bool TakedItem;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Animator animator;
    private const string speedParameter = "Speed";

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float chaseSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.3f;
    [SerializeField] private float waitTime = 60f;

    public NavMeshAgent Agent => agent;
    public Transform Player => player;
    public Transform Target => targetPosition;
    public Animator Animator => animator;

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;
    public float StoppingDistance => stoppingDistance;
    public float WaitTime => waitTime;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    private void Start()
    {
        StateMachine.ChangeState(new MoveToPointState(this));
    }

    private void Update()
    {
        StateMachine.Update();
        animator.SetFloat(speedParameter, agent.velocity.magnitude);
    }
}