using UnityEngine;

public class WaveHandler : MonoBehaviour
{
	#region Vars/Props
	public int Wave { get; private set; } = 1;

    public string sceneWin;

    public float delayWave = 10f;

	public Transform spawnEnemy;
	public GameObject enemyPrefab;
	public float spawnDelay = 1f;
	public bool spawnEnable = true;
	private float enemyDataHealth = 1;
	private float enemyDataSpeed = 1;
	private float enemyDataReward = 5;
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
		//todo to ReadOnly 
		enemyDataHealth = 1f * Wave;
		enemyDataSpeed = 1f + .25f * Wave;
		enemyDataReward = 5f * Wave;
		enemyDataMaxOnWave = 2 * Wave;
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

		enemySpawnedCount++;
	}
	#endregion

	#region Component
	private void Start()
    {
		StartWave();
	}

    private void Update()
    {
        
    }
	#endregion
}