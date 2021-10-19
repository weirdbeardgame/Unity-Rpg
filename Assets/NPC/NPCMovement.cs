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

    Vector3 pointA, pointB;
    //int currentWaypoint = 0;

    // G is the cost from origin to point B. H is the cost from the currently observed tile to the next one. 
    // That can change if there's an obstical or water or any kind of change between points on the ground.
    // F is the final cost between paths
    int G, H, F;// Huristic count between point's A and B

    // Note at somepoint this will have to be changed to reflect the tiled type.
    // Cause I doubt the Tiled map parsers are going to use the same base as the unity types.
    List<Tile> openSpace;
    List<Tile> closedSpace;

    public float speed = 3f;

    private void Start()
    {
        pointA = transform.localPosition;
    }

    void Update()
    {
        // Draw grid. Calculate movement delta's between spaces.
    }
}

