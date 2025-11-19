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
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject winTrigger;
    [SerializeField] private AudioSource footStepAudio;
    [SerializeField] private float stepIntervalWalk = 0.5f;
    [SerializeField] private float stepIntervalRun = 0.35f;
    private const string speedParameter = "Speed";
    private const string wantKill = "WantKill";
    private const string hasKnife = "HasKnife";
    private float stepTimer;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float chaseSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.3f;
    [SerializeField] private float waitTime = 60f;
    [SerializeField] private Dialogue[] dialogue;

    public NavMeshAgent Agent => agent;
    public Transform Player => player;
    public Transform Target => targetPosition;
    public Animator Animator => animator;
    public string WantKill => wantKill;
    public string HasKnife => hasKnife;
    public GameObject Knife => knife;
    public GameObject WinTrigger => winTrigger;
    public bool IsHasKnife =>  animator.GetBool(hasKnife);
    private AudioSource FootStepAudio => footStepAudio;
    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;
    public float StoppingDistance => stoppingDistance;
    public float WaitTime => waitTime;
    public float StepIntervalWalk => stepIntervalWalk;
    public float StepIntervalRun => stepIntervalRun;
    public Dialogue[] Dialogues => dialogue;

    public event System.Action<string> OnStateChanged;

    private void Awake()
    {
        StateMachine = new StateMachine();
        StateMachine.OnStateChanged += StateChangedHandler;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CoffeeCup coffeeCup))
        {
            if (coffeeCup.IsHeld || !coffeeCup.CoffeeIsFull)
                return;

            Destroy(coffeeCup.gameObject);
            TakedItem = true;
        }
    }

    public void HandleFootsteps(float interval)
    {
        if (FootStepAudio == null || !agent.isOnNavMesh) return;

        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0f)
        {
            if (agent.velocity.magnitude < 0.1f)
            {
                FootStepAudio.Stop();
                stepTimer = 0f;
                return;
            }
            FootStepAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            FootStepAudio.Play();

            stepTimer = interval; 
        }
    }

    private void StateChangedHandler(string stateName)
    {
        OnStateChanged?.Invoke(stateName);
    }
}