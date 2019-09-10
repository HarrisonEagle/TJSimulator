using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrontAI : MonoBehaviour
{

   
    private NavMeshAgent agent;

    public Transform goal;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

    }

    void Update()
    {
       
    }
}