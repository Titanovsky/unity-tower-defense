public interface IDamagable
{
    public float Health { get; set; }
	public float Damage { get; set; }

	public void TakeDamage(float damage);
	public void TakeDamage(float damage, object attacker);
	public void Die();
}
