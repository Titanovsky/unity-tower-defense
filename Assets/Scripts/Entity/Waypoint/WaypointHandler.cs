using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
	#region Vars/Props
	public static WaypointHandler Instance;

	[SerializeField] public List<Waypoint> waypoints = new();
	#endregion

	#region Logic
	#endregion

	#region Component
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}
	#endregion
}