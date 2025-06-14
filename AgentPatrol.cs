
using System;
using UdonSharp;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class AgentPatrol : UdonSharpBehaviour
{
    private int currentPatrolPoint = 0;
    [SerializeField] private GameObject[] _patrolPoints;
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(_patrolPoints[currentPatrolPoint].transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Patrol") && currentPatrolPoint < _patrolPoints.Length - 1)
        {
            currentPatrolPoint++;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(_patrolPoints[currentPatrolPoint].transform.position);
        }
        else if(other.gameObject.name.Contains("Patrol") && currentPatrolPoint >= _patrolPoints.Length - 1)
        {
            currentPatrolPoint = 0;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(_patrolPoints[currentPatrolPoint].transform.position);
        }
    }
}
