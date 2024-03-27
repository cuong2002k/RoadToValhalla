using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIControlManager : MonoBehaviour
{

    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;
    private PlayerManager _playerManager;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    public AIMachine AiMachine { get; private set; }

    public Transform target;

    #region  State
    public AIIdleState IdleState { get; private set; }
    public AIWanderState WanderState { get; private set; }
    public AiDeathState DeathState { get; private set; }
    public AIHurtState HurtState { get; private set; }
    #endregion
    private readonly string Idle = "Idle";
    private readonly string Wander = "Wander";
    private readonly string Death = "Death";
    private readonly string Hurt = "Hurt";



    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        AiMachine = new AIMachine();
    }

    private void Start()
    {
        IdleState = new AIIdleState(this, _navMeshAgent, _animator, Idle);
        WanderState = new AIWanderState(this, _navMeshAgent, _animator, Wander);
        DeathState = new AiDeathState(this, _navMeshAgent, _animator, Death);
        HurtState = new AIHurtState(this, _navMeshAgent, _animator, Hurt);

        //_aiMachine.Inittialize(AIWanderState);
        // _aiMachine.ChangeState(AIWanderState);
        AiMachine.Inittialize(IdleState);
        //AiMachine.ChangeState(WanderState);
        _playerManager = PlayerManager.Instance;

    }

    private void Update()
    {
        AiMachine.currentState.LogicUpdate();
        LookPlayer();
    }

    private void LateUpdate()
    {
        AiMachine.currentState.PhysicUpdate();
    }

    private void LookPlayer()
    {
        Vector3 EnemyPos = this.transform.position;
        Vector3 PlayerPos = _playerManager.transform.position - EnemyPos;

        if (PlayerPos.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(PlayerPos.normalized, this.transform.forward) >
            Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                target = _playerManager.transform;
            }
        }
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
