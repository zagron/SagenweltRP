
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

public class FollowPlayer : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] agents;
    [SerializeField] private GameObject waypoint;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fus"))
        {
            foreach (GameObject agent in agents)
            {
                agent.GetComponent<NavMeshAgent>().destination = other.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Fus"))
        {
            foreach (GameObject agent in agents)
            {
                agent.GetComponent<NavMeshAgent>().destination = waypoint.transform.position;
            }
        }
    }
}
