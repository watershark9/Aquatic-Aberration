using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterAI : MonoBehaviour
{
    #region Settings

    [Serializable] private struct NavMeshMovementSettings
    {
        public float movementSpeed;
        public float turnSpeed;
        public float movementAcceleration;
    }

    #endregion
    [Header("Target Settings")]
    [SerializeField] private GameObject target;
    
    [Header("Search Settings")]
    [SerializeField] private Transform[] searchPoints;

    [Header("Movement Settings")]
    [SerializeField] private NavMeshMovementSettings searchingMovementSettings;
    
    [Header("Chase Settings")]
    [SerializeField] private float chaseTimeout = 4f;
    [SerializeField] private NavMeshMovementSettings chasingMovementSettings;
    
    private NavMeshAgent _navMeshAgent;
    private CharacterVision _vision;
    private MonsterState _currentState;
    private bool _canSeeTarget = false;
    private DateTime _lastSeenTargetTime;
    
    public readonly UnityEvent<Vector3> Alerted = new();
    public readonly UnityEvent TargetSpotted = new();
    public readonly UnityEvent<MonsterState> StateChanged = new();
    
    #region Vision
    
    private IEnumerator CheckVisionCoroutine()
    {
        while (_vision.isActiveAndEnabled)
        {
            _canSeeTarget = _vision.IsObjectVisible(target);
            if (_canSeeTarget)
            {
                if (_currentState != MonsterState.Chasing)
                    TargetSpotted?.Invoke();
                _lastSeenTargetTime = DateTime.Now;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion
    
    #region State Machine

    public enum MonsterState
    {
        Searching,
        Chasing,
        Investigating,
    }

    private void SetStateSettings(MonsterState state)
    {
        var movementSettings = state switch
        {
            MonsterState.Chasing => chasingMovementSettings,
            _ => searchingMovementSettings
        };

        _navMeshAgent.speed = movementSettings.movementSpeed;
        _navMeshAgent.acceleration = movementSettings.movementAcceleration;
        _navMeshAgent.angularSpeed = movementSettings.turnSpeed;
    }

    private void Alert(Vector3 coordinate)
    {
        ChangeState(MonsterState.Investigating);
        _navMeshAgent.SetDestination(coordinate);
    }
    
    private void LocateTarget()
    {
        ChangeState(MonsterState.Chasing);
    }

    private void ChangeState(MonsterState state)
    {
        _currentState = state;
        StateChanged?.Invoke(state);
    }
    
    private void StateMachineLogic()
    {
        switch (_currentState)
        {
            case MonsterState.Searching:
            {
                SearchLogic();
                break;
            }
            case MonsterState.Chasing:
            {
                ChaseLogic();
                break;
            }
            case MonsterState.Investigating:
            {
                InvestigateLogic();
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return;

        void ChaseLogic()
        {
            var timeSpan = DateTime.Now - _lastSeenTargetTime;
            if (timeSpan.Seconds > chaseTimeout)
            {
                ChangeState(MonsterState.Searching);
                return;
            }

            if (!_canSeeTarget) return;
            _navMeshAgent.SetDestination(target.transform.position);
        }

        void SearchLogic()
        {
            if (_navMeshAgent.pathPending ||
                !(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) return;

            var randomSearchPoint = searchPoints.GetRandomElement();
            _navMeshAgent.SetDestination(randomSearchPoint.position);
        }

        void InvestigateLogic()
        {
            if (_navMeshAgent.pathPending ||
                !(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)) return;

            ChangeState(MonsterState.Searching);
        }
    }
    
    #endregion

    #region Monobehaviour

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _vision = GetComponent<CharacterVision>();
        
        Alerted?.AddListener(Alert);
        TargetSpotted?.AddListener(LocateTarget);
        StateChanged?.AddListener(SetStateSettings);

#if DEBUG
        StateChanged?.AddListener((state) =>
        {
            Debug.Log($"Monster state changed to {state}");
        });
#endif
    }

    private void Start()
    {
        ChangeState(MonsterState.Searching);

        StartCoroutine(CheckVisionCoroutine());
    }
    
    private void FixedUpdate()
    {
        StateMachineLogic();
    }

    #endregion
}