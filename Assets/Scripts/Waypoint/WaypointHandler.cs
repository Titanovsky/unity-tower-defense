using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
	#region Vars/Props
	public static WaypointHandler Instance;

	[SerializeField] public List<Waypoint> waypoints = new();

	[Header("Spawn Enemy")]
	public Transform spawnEnemy;
	public GameObject enemyPrefab;
	public float spawnDelay = 1f;
	public bool spawnEnable = true;
	#endregion

	#region Logic
	private void StartSpawnEnemies()
	{
		InvokeRepeating(nameof(SpawnEnemy), 0f, spawnDelay);
	}

	private void StopSpawnEnemies()
	{
		CancelInvoke();
	}

	private void SpawnEnemy()
	{
		if (!spawnEnable) return;
		if (!enemyPrefab) { Debug.LogError($"[Waypoint Handler] Prefab doesn't exist"); return; }
		if (!spawnEnemy) { Debug.LogError($"[Waypoint Handler] Spawn Enemy Transform doesn't exist"); return; }

		var enemy = Instantiate(enemyPrefab, spawnEnemy.position, spawnEnemy.rotation);
	}
	#endregion

	#region Component
	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		StartSpawnEnemies();
	}
	#endregion
}
//