using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWidget : MonoBehaviour
{
    public string sceneStart = "PlayScene";

    public void ChangeToStartScene()
    {
		if (string.IsNullOrEmpty(sceneStart)) return;

		SceneManager.LoadScene(sceneStart);
	}

	public void CloseGame()
	{
		Application.Quit();
	}
}
