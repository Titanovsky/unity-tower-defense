using System;

public interface IDamagable
{
	public event Action<float, float> OnChangedHealth; // newHealth, oldHealth
	public event Action OnDead;

	public float Health { get; set; }
	public float Damage { get; set; }

	public void TakeDamage(float damage);
	public void TakeDamage(float damage, object attacker);
	public void Die();
}