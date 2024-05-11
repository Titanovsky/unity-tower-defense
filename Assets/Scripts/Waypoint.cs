using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	public static Color gizmoColor = Color.red;
	public static float gizmoRadius = 0.22f;

	public void OnDrawGizmos()
	{
       Gizmos.color = gizmoColor;
       Gizmos.DrawWireSphere(transform.position, gizmoRadius);
	}
}
