using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowObjective : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _objective;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Go();
    }

    private void Go()
    {
        Go(_objective.position);
    }

    private void Go(Vector3 destination)
    {
        _agent.SetDestination(_objective.position);
    }

    public void ChangeObjective(Transform newObjective)
    {
        _objective = newObjective;
    }
}
