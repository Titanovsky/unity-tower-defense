using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlatformBuy : MonoBehaviour
{
    #region Vars/Props
    public float cost = 0f;

	public Text textCost;
    public float addPosX = 10f;
	public float addPosY = 50f;

    public Button buttonBuy;

	public GameObject towerPrefab;
    public float angleZ = 0f;

    private Player ply;

    public Camera mainCamera;
	#endregion

	#region Logic
	public void BuyTower()
    {
        if (ply.Money < cost) return;

        ply.Money -= cost;

        SpawnTower();
        Destroy(gameObject);
    }

    public void SpawnTower()
    {
        if (towerPrefab == null) return;

        GameObject obj = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        obj.transform.rotation = new Quaternion(obj.transform.rotation.x, obj.transform.rotation.y, angleZ, obj.transform.rotation.w);
	}

    public void SetupPositionUI()
    {
        if (mainCamera == null) return;

        Vector3 pos = mainCamera.ScreenToWorldPoint(transform.position);

        Vector3 posText = new(pos.x + addPosX, pos.y + addPosY, pos.z);
        textCost.rectTransform.position = posText;

		buttonBuy.transform.position = pos;
	}
	#endregion

	#region Component
	private void Start()
	{
        ply = Player.Instance;

        SetupPositionUI();
	}
	#endregion
}