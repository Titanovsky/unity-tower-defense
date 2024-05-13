using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{	
	#region Vars/Props
	public TowerState State { get; private set; } = TowerState.Free;

	public TowerRotator rotator;
	public Quaternion defaultRotationRotator;

	public Enemy target;
	public Enemy nextTarget;

	public Transform spawnProjectileTransform;
	public GameObject projectilePrefab;
	public float projectileSpeed = 25f;
	public float projectileDelay = 1f;
	public float projectileTimerDelay = 0f;
	public float projectileLifeTime = 2f;
	public bool projectileCanShoot = true;

	private new SphereCollider collider;
	#endregion

	#region Logic
	private void Init()
	{
		if (rotator == null)
			rotator = GetComponentInChildren<TowerRotator>(true);

		if (collider == null)
			collider = GetComponent<SphereCollider>();

		defaultRotationRotator = rotator.transform.rotation;
	}

	private void SetTarget(Enemy targetEnemy)
	{
		target = targetEnemy;
	}

	private void StartShooting()
	{
		if (State == TowerState.Busy) return;
		if (target == null) return;

		State = TowerState.Busy;
		target.OnDead += ResetTowerAfterEnemyDead;

		Debug.Log($"[Tower] Start Shooting");
	}

	private void Shooting()
	{
		if (State != TowerState.Busy) return;
		if (target == null) return;

		rotator.Rotate(target.transform);
		Shoot();
	}

	public void Shoot()
	{
		if (projectilePrefab == null || spawnProjectileTransform == null) return;
		if (projectileCanShoot == false)
		{
			projectileTimerDelay += Time.deltaTime;
			if (projectileTimerDelay < projectileDelay) return;

			projectileTimerDelay = 0;
			projectileCanShoot = true;
		}

		var projectile = Instantiate(projectilePrefab, spawnProjectileTransform.position, spawnProjectileTransform.rotation);
		projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
		projectile.GetComponent<Bullet>().owner = gameObject;

		Destroy(projectile, projectileLifeTime);

		Debug.Log($"[Tower] Shoot");

		projectileCanShoot = false;

		if (target == null) StopShooting();
	}

	private void StopShooting()
	{
		if (State == TowerState.Free) return;
		if (nextTarget != null)
		{
			target = nextTarget;
			target.OnDead += ResetTowerAfterEnemyDead;
			nextTarget = null;

			return;
		}

		State = TowerState.Free;

		target.OnDead -= ResetTowerAfterEnemyDead;
		target = null;
		projectileCanShoot = true;
		rotator.transform.rotation = defaultRotationRotator;

		Debug.Log($"[Tower] Stop Shooting");
	}

	private void ResetTowerAfterEnemyDead()
	{
		StopShooting();
	}
	#endregion

	#region MonoBehaviour
	private void Start()
	{
		Init();
	}

	private void Update()
	{
		Shooting();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (rotator == null) return;
		if (!other.TryGetComponent<Enemy>(out var enemy)) return;

		if (target == null)
		{
			Debug.Log($"[Tower] new target {enemy}");

			target = enemy;

			StartShooting();
		} 
		else
		{
			nextTarget = enemy;

			Debug.Log($"[Tower] new next target {enemy}");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (rotator == null) return;
		if (!other.TryGetComponent<Enemy>(out var enemy)) return;

		if (enemy == nextTarget)
		{
			nextTarget = null;

			Debug.Log($"[Tower] removed next target {enemy}");
		}
		else if (enemy == target)
		{
			StopShooting();
		}
	}
	#endregion
}

public enum TowerState
{
	Busy,
	Free
}