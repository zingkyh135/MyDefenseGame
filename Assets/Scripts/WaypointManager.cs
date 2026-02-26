using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;
    public Transform[] waypoints;

    void Awake()
    {
        instance = this;
        Transform group = transform;
        waypoints = new Transform[group.childCount];
        for (int i = 0; i < group.childCount; i++)
        {
            waypoints[i] = group.GetChild(i);
        }
    }
}
