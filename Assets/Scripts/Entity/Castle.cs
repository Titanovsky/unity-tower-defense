using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour, IDamagable
{
	#region Vars/Props
	[SerializeField] private float _health = 1f;
	public float Health
	{
		get
		{
			return _health;
		}

		set
		{
			float oldHealth = _health;
			_health = value;

			OnChangedHealth?.Invoke(_health, oldHealth);
		}
	}

	[SerializeField] private float _damage = 1f;
	public float Damage
	{
		get { return _damage; }
		set { _damage = value; }
	}

	public event Action<float, float> OnChangedHealth;
	public event Action OnDead;

	public string sceneFail;
	#endregion

	#region Logic
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

		Debug.Log($"Attacker {attacker} gave {dmg} damage to {this}");
	}

	public void Die()
	{
		Destroy(gameObject);

		OnDead?.Invoke();
	}
	#endregion

	#region Component
	private void Start()
    {
		// плохой пример, в одной системе делаем подписку на ивент, который можем переписать в Die (из-за интерфейсов такое)
		OnDead += () => 
		{
			if (string.IsNullOrEmpty(sceneFail)) return;

			SceneManager.LoadScene(sceneFail);
		};
	}

	private void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<Enemy>(out var enemy)) return;

		var obj = other.gameObject;

		TakeDamage(obj.GetComponent<IDamagable>().Damage, obj);
		Destroy(obj);
	}
	#endregion
}