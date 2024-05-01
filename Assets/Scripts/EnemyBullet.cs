using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject owner;
    public float speed;

    private GameObject target;
    private Player ply;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        ply = target.GetComponent<Player>();

        Vector3 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(moveDir.x, 0, moveDir.z);
        Destroy(gameObject, 1f);
    }

	private void OnCollisionEnter(Collision col)
	{
        var obj = col.gameObject;

        if (obj == target)
        {
            Enemy enemy = owner.GetComponent<Enemy>();

            ply.TakeDamage(enemy.Damage, enemy);
        }
	}
}
