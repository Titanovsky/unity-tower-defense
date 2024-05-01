using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform transformGeneratorBullets;
    public GameObject projectile;
    public float speed = 25f;
    public float delay = 0.1f;
    public float timeLifeBullet = 2.25f;

    private bool canShoot;
    
    private void Start()
    {
        canShoot = true;
    }

    private void Attack()
    {
        if (!canShoot) return;

        canShoot = false;

        var bullet = Instantiate(projectile, transformGeneratorBullets.position, transformGeneratorBullets.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
        bullet.GetComponent<Bullet>().owner = gameObject;

        Destroy(bullet, timeLifeBullet);
        StartCoroutine(Reload());
	}

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(delay);

        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Attack();
    }
}
