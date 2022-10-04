using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchForWaypoint : IState
{
    private readonly mothmanController _mothmanController;
    public SearchForWaypoint( mothmanController mothmanController) => _mothmanController = mothmanController;

    public void Tick()
    {
        // if(_mothmanController.wayPointReached) {
        _mothmanController.WaypointTarget = FindNextWaypoint(5);
        //}
    }

    private Waypoint FindNextWaypoint(int pickFromNearest)
    {
        return Object
            .FindObjectsOfType<Waypoint>()
            .OrderBy(t => Vector3.Distance(_mothmanController.transform.position, t.transform.position))
            .Take(pickFromNearest)
            .OrderBy(t => Random.Range(0, int.MaxValue))
            .FirstOrDefault(t => Vector3.Distance(_mothmanController.transform.position, t.transform.position) <= _mothmanController.maxDistanceToWaypoint);
    }
    

    public void OnEnter() { }

    public void OnExit() { }
}
