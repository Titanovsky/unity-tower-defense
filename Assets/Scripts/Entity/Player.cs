using System;
using UnityEngine; 

public class Player : MonoBehaviour
{
	#region Vars/Props
	public static Player Instance;

	public uint Frags { get; set; } = 0;

	[SerializeField] private float _money = 0f;
	public float Money 
	{
		get => _money;

		set
		{
			if (value < 0f) value = 0f;

			var oldValue = _money;
			_money = value;

			OnChangedMoney?.Invoke(value, oldValue);
		}
	}
	public event Action<float, float> OnChangedMoney;
	#endregion

	#region Component
	private void Awake()
	{
		if (Instance == null) Instance = this;
	}
	#endregion
}