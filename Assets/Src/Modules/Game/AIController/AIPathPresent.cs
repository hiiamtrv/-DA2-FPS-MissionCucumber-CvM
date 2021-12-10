using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathPresent : MonoBehaviour
{
    LineRenderer line; //to hold the line Renderer
    NavMeshAgent agent; //to hold the agent of this gameObject

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>(); //get the line renderer
        agent = GetComponent<NavMeshAgent>(); //get the agent
    }

    void FixedUpdate()
    {
        line.SetPosition(0, transform.position);
        DrawPath(agent.path);
    }

    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2) //if the path has 1 or no corners, there is no need
            return;

        line.SetVertexCount(path.corners.Length); //set the array of positions to the amount of corners

        for (var i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }
    }
}
