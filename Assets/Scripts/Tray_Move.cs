using UnityEngine;
using UnityEngine.SceneManagement;

public class Tray_Move : MonoBehaviour
{
	public int num = 1;
	
	public void LoadSceneByName(string sceneName)
	{
		if (!string.IsNullOrEmpty(sceneName)) SceneManager.LoadScene(sceneName);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Title_Icon"))
		{
			if (num == 1) LoadSceneByName("Main");//���� ������
			else if (num == 2) LoadSceneByName("Main");
			else if (num == 3) Application.Quit();
		}
	}
}
