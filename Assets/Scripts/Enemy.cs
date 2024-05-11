using Unity.VisualScripting;
using UnityEngine;

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

	#region Logic
	public void TakeDamage(float dmg)
	{
		float newHP = Health - dmg;
		if (newHP > 0) { Health = newHP; return; }

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

    public void Rotate()
    {
		if (target == null) return;

		Vector3 targetDirection = (target.transform.position - transform.position).normalized;

		transform.rotation = Quaternion.LookRotation(targetDirection);
	}

	public void Move()
	{
		if (target == null) return;

		var newPos = new Vector3(transform.forward.x, 0, transform.forward.z);
		transform.position += newPos * Time.deltaTime * speed;
	}

	public void FinishWaypoint()
	{
		if (waypointHandler == null) return;

		targetID++;
		Debug.Log($"{targetID} {waypointHandler.waypoints.Count}");
		if (waypointHandler.waypoints.Count <= targetID) return;

		var waypoint = waypointHandler.waypoints[targetID];
		if (waypoint == null) return;

		Debug.Log($"c");

		target = waypoint;
		Rotate();
	}
	#endregion

		#region Monobeh
	private void Start()
	{
		waypointHandler = WaypointHandler.Instance;
		if (waypointHandler == null) return;

		target = waypointHandler.waypoints[targetID];
		if (target == null) return;

		Rotate();
	}

	private void Update()
    {
		Move();
	}

	public void OnTriggerEnter(Collider other)
	{
		var waypoint = other.GetComponent<Waypoint>();
		Debug.Log("a");
		if (waypoint == null || waypoint != target) return;
		Debug.Log("b");

		FinishWaypoint();
	}
	#endregion
}