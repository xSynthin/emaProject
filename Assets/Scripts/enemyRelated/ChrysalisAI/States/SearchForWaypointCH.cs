using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchForWaypointCH : IState
{
    private readonly chrysalisController _chrysalisController;
    public SearchForWaypointCH( chrysalisController chrysalisController) => _chrysalisController = chrysalisController;
    public void Tick()
    {
        // if(_mothmanController.wayPointReached) {
        _chrysalisController.WaypointTarget = FindNextWaypoint(5);
        //}
    }
    private Waypoint FindNextWaypoint(int pickFromNearest)
    {
        return Object.FindObjectsOfType<Waypoint>()
            .OrderBy(t => Vector3.Distance(_chrysalisController.transform.position, t.transform.position))
            .Take(pickFromNearest)
            .OrderBy(t => Random.Range(0, int.MaxValue))
            .FirstOrDefault();
    }
    public void OnEnter() { }

    public void OnExit() { }
}
