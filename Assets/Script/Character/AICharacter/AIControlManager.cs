using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIControlManager : MonoBehaviour
{
    [Header("Data")]
    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;
    public float attackRange = 2f;
    public float attackTimer = 2f;
    public CountDownTimer AttackCountDown;
    public float timeFocus = 5f;
    public bool isRunning = false;
    public bool lookPlayer = false;


    [Header("Component")]
    private PlayerManager _playerManager;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    [Header("Player target")]
    public Transform PlayerTarget;

    #region State Machine
    public AIMachine AiMachine { get; private set; }
    public AIIdleState IdleState { get; private set; }
    public AIWanderState WanderState { get; private set; }
    public AiDeathState DeathState { get; private set; }
    public AIHurtState HurtState { get; private set; }
    public AIChaseState ChaseState { get; private set; }
    public AIAttackState AttackState { get; private set; }
    #endregion

    [Header("Animation string")]
    private readonly string Idle = "Idle";
    private readonly string Wander = "Wander";
    private readonly string Death = "Death";
    private readonly string Hurt = "Hurt";
    private readonly string Attack = "Attack";




    private void Awake()
    {
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        AiMachine = new AIMachine(); // create state machine
    }

    private void Start()
    {
        IdleState = new AIIdleState(this, _navMeshAgent, _animator, Idle);
        WanderState = new AIWanderState(this, _navMeshAgent, _animator, Wander);
        DeathState = new AiDeathState(this, _navMeshAgent, _animator, Death);
        HurtState = new AIHurtState(this, _navMeshAgent, _animator, Hurt);
        ChaseState = new AIChaseState(this, _navMeshAgent, _animator, Wander);
        AttackState = new AIAttackState(this, _navMeshAgent, _animator, Attack);
        AiMachine.Inittialize(IdleState);
        _playerManager = PlayerManager.Instance;
        AttackCountDown = new CountDownTimer(attackTimer);

    }

    private void Update()
    {
        AiMachine.currentState.LogicUpdate();
        lookPlayer = LookPlayer(); // check can see player
        AttackCountDown.Tick(Time.deltaTime);
        if (_navMeshAgent.enabled)
        {
            float remainingDistance = Vector3.Distance(_playerManager.transform.position, transform.position);
            isRunning = (remainingDistance > _navMeshAgent.stoppingDistance);
        }
    }

    private void LateUpdate()
    {
        AiMachine.currentState.PhysicUpdate();
    }

    public bool LookPlayer()
    {
        Vector3 EnemyPos = this.transform.position;
        Vector3 PlayerPos = _playerManager.transform.position - EnemyPos;

        if (PlayerPos.magnitude <= detectionRadius) // check if player in range detaction
        {
            if (Vector3.Dot(PlayerPos.normalized, this.transform.forward) >
            Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                PlayerTarget = _playerManager.transform;
                return true;
            }
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 0, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);

    }
#endif

}
