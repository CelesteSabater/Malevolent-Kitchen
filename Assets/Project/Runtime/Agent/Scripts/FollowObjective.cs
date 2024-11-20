using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowObjective : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform objective;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Go();
    }

    public void Go()
    {
        Go(objective.position);
    }

    public void Go(Vector3 destination)
    {
        agent.SetDestination(objective.position);
    }

    public void Teleport(Vector3 destination)
    {
        this.transform.position = destination;
    }
}
