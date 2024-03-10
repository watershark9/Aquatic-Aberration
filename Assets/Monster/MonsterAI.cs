using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] [TagField] private string targetTag = "Player";
    
    private GameObject _target;
    private NavMeshAgent _navMeshAgent;
    private CharacterVision _vision;

    private enum MonsterState
    {
        Searching,
        Chasing,
        Hunting
    }

    private MonsterState _currentState;

    public void GoToPoint(Vector3 coordinate)
    {
        _navMeshAgent.SetDestination(coordinate);
    }
    
    private void Awake()
    {
        _target = GameObject.FindWithTag(targetTag);
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _vision = GetComponent<CharacterVision>();
    }

    private void StateMachineLogic()
    {
        switch (_currentState)
        {
            case MonsterState.Searching:
            {
                break;
            }
            case MonsterState.Chasing:
            {
                break;
            }
            case MonsterState.Hunting:
            {
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void FixedUpdate()
    {
        StateMachineLogic();
    }
}
