using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
	public static WaypointHandler Instance;

	[SerializeField] public List<Waypoint> waypoints = new();

	private void Awake()
	{
		Instance = this;
	}
}