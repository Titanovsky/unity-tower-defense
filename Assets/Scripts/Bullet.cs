using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;

    void OnCollisionEnter(Collision col)
    {
        var obj = col.gameObject;
        Enemy enemy;

        if (obj.TryGetComponent(out enemy))
        {
            Destroy(gameObject);

            enemy.TakeDamage(enemy.Damage, owner);
		}
    }
}
