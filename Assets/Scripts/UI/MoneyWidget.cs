using UnityEngine;
using UnityEngine.UI;

public class MoneyWidget : MonoBehaviour
{
	#region Vars/Props
	private Player ply;

	public Image panel;
	private float wMaxSizePanel;
	private float hMaxSizePanel;

	public Text text;
	#endregion

	#region Logic
	public void UpdateText(float newValue = 0, float oldValue = 0)
	{
		SetText(ply.Money);
		SetPanel();
	}

	private void SetText(float money)
	{
		var moneyText = Mathf.RoundToInt(money);

		text.text = $"{moneyText}$";
	}

	private void SetPanel()
	{
		//todo todo
	}
	#endregion

	#region Component
	private void Start()
	{
		if (ply == null)
			ply = Player.Instance;

		if (text == null)
			text = GetComponentInChildren<Text>();

		if (panel == null)
			panel = GetComponentInChildren<Image>();

		ply.OnChangedMoney += UpdateText;
	}
	#endregion
}