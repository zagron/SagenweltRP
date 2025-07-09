
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class FollowPlayer : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] agents;

    private void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        foreach (GameObject agent in agents)
        {
            agent.GetComponent<NavMeshAgent>().destination = player.GetPosition();
        }
    }
    private void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        foreach (GameObject agent in agents)
        {
            agent.GetComponent<AgentPatrol>().SetPatrolPoint();
        }
    }
}
