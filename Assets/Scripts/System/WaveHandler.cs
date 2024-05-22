using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
	#region Vars/Props
	public int Wave { get; private set; } = 1;

    public string sceneWin;

	public float delayTheFirstWave = 3.4f;
	public float delayWave = 10f;

	public Transform spawnEnemy;
	public GameObject enemyPrefab;
	public float spawnDelay = 1f;
	public bool spawnEnable = true;
	public List<Color32> randomColors = new();
	public Color32 color = new Color32(255, 0, 0, 255);

	public float enemyDataHealthMultiply = 3f;
	private float enemyDataHealth = 1f;
	public float enemyDataSpeedMultiply = .25f;
	private float enemyDataSpeed = 1f;
	public float enemyDataRewardMultiply = 5f;
	private float enemyDataReward = 5f;
	public int enemyDataMaxOnWaveMultiply = 2;
	private int enemyDataMaxOnWave = 1;

	private int enemySpawnedCount = 0;
	private int enemySpawnedKilled = 0;
	#endregion

	#region Logic
	private void StartWave()
    {
        enemySpawnedCount = 0;
        enemySpawnedKilled = 0;

		SetupEnemyData();
		InvokeRepeating(nameof(SpawnEnemy), 0f, spawnDelay);
	}

    private void StopWave()
    {
		CancelInvoke();
	}

	private void NextWave()
	{
		enemySpawnedKilled++;
		if (enemySpawnedKilled != enemyDataMaxOnWave) return;

		StopWave();
		Wave++;

		Invoke(nameof(StartWave), delayWave);
	}

	private void SetupEnemyData()
	{
		enemyDataHealth = enemyDataHealthMultiply * Wave;
		enemyDataSpeed = enemyDataSpeedMultiply * Wave;
		enemyDataReward = enemyDataRewardMultiply * Wave;
		enemyDataMaxOnWave = enemyDataMaxOnWaveMultiply * Wave;

		if (randomColors.Count > 0)
			color = randomColors[Random.Range(0, randomColors.Count - 1)];
	}

	private void SpawnEnemy()
	{
		if (!spawnEnable) return;
		if (enemySpawnedCount >= enemyDataMaxOnWave) return;
		if (!enemyPrefab) { Debug.LogError($"[Waypoint Handler] Prefab doesn't exist"); return; }
		if (!spawnEnemy) { Debug.LogError($"[Waypoint Handler] Spawn Enemy Transform doesn't exist"); return; }

		var obj = Instantiate(enemyPrefab, spawnEnemy.position, spawnEnemy.rotation);

		Enemy enemy = obj.GetComponent<Enemy>();
		enemy.MoneyReward = enemyDataReward;
		enemy.speed = enemyDataSpeed;

		IDamagable damagable = enemy;
		damagable.Health = enemyDataHealth;
		damagable.OnDead += NextWave;

		MeshRenderer model = obj.GetComponent<MeshRenderer>();
		model.material.color = color;

		enemySpawnedCount++;
	}
	#endregion

	#region Component
	private void Start()
    {
		Invoke(nameof(StartWave), delayTheFirstWave);
	}

    private void Update()
    {
        
    }
	#endregion
}