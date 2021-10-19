using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Might kinda be used with Tiled
[CreateAssetMenu(menuName = "New Waypoint")]
public class Waypoint : ScriptableObject
{
    int waypointID;
    public GameObject point;
    public NPCData follower;
}
