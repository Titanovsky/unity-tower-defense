using UnityEngine; 

public class TowerRotator : MonoBehaviour
{
	public void Rotate(Transform target)
	{
		transform.LookAt(target);
	}
}