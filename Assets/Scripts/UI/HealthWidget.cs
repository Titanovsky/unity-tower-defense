using UnityEngine;
using UnityEngine.UI;

public class HealthWidget : MonoBehaviour
{
	#region Vars/Props
	public IDamagable damagable;

    private float maxHealth = 0f;

    [SerializeField] private Image imgHealth;
	private float wMaxSizePanel;
	private float hMaxSizePanel;
	#endregion

	#region Logic
	private void ChangeSizeImageHealth(float newHealth, float oldHealth)
	{
		float w = (newHealth > maxHealth) ? wMaxSizePanel : (wMaxSizePanel / maxHealth) * newHealth;

		imgHealth.rectTransform.sizeDelta = new Vector2(w, hMaxSizePanel);
	}
	#endregion

	#region Component
	private void Start()
    {
		if (damagable == null)
			damagable = GetComponentInParent<IDamagable>();

		maxHealth = damagable.Health;
        wMaxSizePanel = imgHealth.rectTransform.sizeDelta.x;
		hMaxSizePanel = imgHealth.rectTransform.sizeDelta.y;

		damagable.OnChangedHealth += ChangeSizeImageHealth;
	}
	#endregion
}