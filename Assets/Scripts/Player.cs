using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
	public delegate void dSetHealth(float health);
	public event dSetHealth OnSetHealth;

	private float _hp = 10f;
	public float Health
	{
		get
		{
			return _hp;
		}
		set
		{
			_hp = value;
			OnSetHealth(value);
		}
	}
	public float Damage { get; set; } = 2f;

	public void TakeDamage(float dmg)
    {
        float newHP = Health - dmg;
        if (newHP > 0) { Health = newHP; return; }

        Die();
    }

	public void TakeDamage(float dmg, object attacker)
	{
        TakeDamage(dmg);

        Debug.Log($"Attacker {attacker} gave {dmg} damage to player");
	}

	public void Die()
    {
        Destroy(gameObject);
    }
}
