
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class FollowPlayer : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] agents;
    void Start()
    {
        
    }

    private void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            foreach (GameObject agent in agents)
            {
                agent.GetComponent<NavMeshAgent>().destination = player.GetPosition();
            }
        }
    }
    private void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            foreach (GameObject agent in agents)
            {
                agent.GetComponent<AgentPatrol>().SetPatrolPoint();
            }
        }
    }
}
