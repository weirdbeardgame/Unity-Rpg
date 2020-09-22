using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class NPCMovement : MonoBehaviour
{
    public List<Vector3> waypoints; // A List of tagged waypoints

    //Vector3 currentWaypoint;
    int currentWaypoint = 0;

    public float speed = 3f;

    private void Start()
    {

        //currentWaypoint = waypoints[0];
    }

    void Update()
    {
        if (waypoints.Count > 0)
        {
            if ((transform.position - waypoints[currentWaypoint]).sqrMagnitude < 0.01f)
            {
                currentWaypoint++;
                currentWaypoint %= waypoints.Count; // Is supposed to check if in bounds?
            }
       
            Vector3 moveDirection = (waypoints[currentWaypoint] - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime; 
        }
    }
}

