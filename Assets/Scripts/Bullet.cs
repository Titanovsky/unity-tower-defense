using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;

    void OnCollisionEnter(Collision col)
    {
        var obj = col.gameObject;
        if (obj.TryGetComponent(out IDamagable objDamageInterface))
        {
            Destroy(gameObject);

			objDamageInterface.TakeDamage(objDamageInterface.Damage, owner);
		}
    }
}
