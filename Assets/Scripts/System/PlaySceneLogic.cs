using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneLogic : MonoBehaviour
{
	#region Vars/Props
	public int finalWave = 5;
	public string winScene = "WinScene";
	#endregion

	#region Component
	private void Start()
    {
		var waveHandler = WaveHandler.Instance;

		waveHandler.OnNextWave += () => 
		{
			if (waveHandler.Wave > finalWave && !string.IsNullOrEmpty(winScene))
				SceneManager.LoadScene(winScene);
		};
    }
	#endregion
}
