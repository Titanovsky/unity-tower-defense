using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Enemy : MonoBehaviour, IDamagable
{
	#region Vars/Props
	public float Health { get; set; } = 6f;
	public float Damage { get; set; } = 1f;

	public float speed = 0.1f;

    public Waypoint target;
    private WaypointHandler waypointHandler;
	private int targetID = 0;
	#endregion

	#region IDamagable
	public void TakeDamage(float dmg)
	{
		float newHP = Health - dmg;
		if (newHP > 0) 
		{
			Health = newHP; 
			return;
		}

		Die();
	}

	public void TakeDamage(float dmg, object attacker)
	{
		TakeDamage(dmg);

		Debug.Log($"Attacker {attacker} gave {dmg} damage to enemy");
	}

	public void Die()
	{
		Destroy(gameObject);
	}
	#endregion

	#region Logic
	private void Init()
	{
		waypointHandler = WaypointHandler.Instance;
		if (waypointHandler == null) return;
		if (waypointHandler.waypoints.Count <= targetID) return;

		target = waypointHandler.waypoints[targetID];

		//Debug.Log($"Enemy {gameObject} initialized");
	}

	private void Rotate()
    {
		if (target == null) return;

		transform.LookAt(target.transform);

		//Debug.Log($"Rotated to {target}");
	}

	private void Move()
	{
		if (target == null) return;

		var newPos = new Vector3(transform.forward.x, 0, transform.forward.z);
		transform.position += newPos * Time.deltaTime * speed;
	}

	private void SetTarget(Waypoint targetWaypoint)
	{
		target = targetWaypoint;

		//Debug.Log($"Set target {target}");
	}

	private void RemoveTarget()
	{
		Waypoint oldTarget = target;

		target = null;

		//Debug.Log($"Removed target {oldTarget}");
	}

	private void FinishWaypoint()
	{
		if (waypointHandler == null) return;

		RemoveTarget();

		targetID++;
		if (waypointHandler.waypoints.Count <= targetID) return;

		//Debug.Log($"Finished Waypoint, next: {target}");

		SetTarget(waypointHandler.waypoints[targetID]);
		Rotate();
	}
	#endregion

	#region MonoBehavior
	private void Start()
	{
		Init();
		Rotate();
	}

	private void Update()
    {
		Move();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<Waypoint>(out var waypoint) || waypoint != target) return;

		FinishWaypoint();
	}
	#endregion
}