using UnityEngine;
using UnityEngine.UI;

public class EntityUI : MonoBehaviour
{
    //todo change this to class Entity
    public Player ply;

    private float maxHealth = 0f;

    [SerializeField] private Image imgHealth;
	private float wMaxSizePanel;
	private float hMaxSizePanel;

	private void Start()
    {
        maxHealth = ply.Health;
        wMaxSizePanel = imgHealth.rectTransform.sizeDelta.x;
		hMaxSizePanel = imgHealth.rectTransform.sizeDelta.y;

		ply.OnSetHealth += ChangeSizeImageHealth;
	}

    private void ChangeSizeImageHealth(float health)
    {
        float w = (health > maxHealth) ? wMaxSizePanel : (wMaxSizePanel / maxHealth) * health;

		imgHealth.rectTransform.sizeDelta = new Vector2(w, hMaxSizePanel);
	}
}
