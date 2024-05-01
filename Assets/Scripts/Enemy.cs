using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float nextFireTime;
    public GameObject projectile;
    public Transform posGeneratorBullets;

	private float fireRate = 1f;
    private Transform playerTransform;

	public float Health { get; set; } = 6f;
	public float Damage { get; set; } = 1f;
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



	private void Start()
    {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Attack()
    {
        var bullet = Instantiate(projectile, posGeneratorBullets.position, posGeneratorBullets.rotation);
        bullet.GetComponent<EnemyBullet>().owner = gameObject;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        transform.LookAt(playerTransform);

        float distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        } 
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
		Gizmos.DrawWireSphere(transform.position, shootingRange);
	}
}
