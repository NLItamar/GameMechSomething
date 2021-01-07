﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    //change this to gameobject list and set order on editor
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private bool mustPatrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.enabled = true;

        if(points.Length == 0)
        {
            mustPatrol = false;
        }
        else
        {
            mustPatrol = true;
        }

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        if(mustPatrol)
        {
            GotoNextPoint();
        }
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && mustPatrol)
        {
            GotoNextPoint();
        }
    }
}
