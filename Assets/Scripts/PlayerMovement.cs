using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedMove = 5f;
	public float speedRot = 200f;

	// Update is called once per frame
	void Update()
    {
        float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		float delta = Time.deltaTime;

		transform.Translate(0, 0, v * delta * speedMove);
		transform.Rotate(0, h * delta * speedRot, 0);
	}
}
