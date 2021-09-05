using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Waypoint")]
public class Waypoint : ScriptableObject
{
    int waypointID;
    public GameObject point;
    public NPCData follower;
}
