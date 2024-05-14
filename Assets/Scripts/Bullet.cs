using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;

    private void OnTriggerEnter(Collider col)
    {
        var obj = col.gameObject;
        if (obj.TryGetComponent(out IDamagable objDamageInterface))
        {
            Destroy(gameObject);

			objDamageInterface.TakeDamage(objDamageInterface.Damage, owner);
		}
    }
}
