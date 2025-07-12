using UnityEngine;

public class Tray_Move : MonoBehaviour
{
	public int num = 1;
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Title_Icon"))
		{
			StageManager stageManager = FindObjectOfType<StageManager>();
			if (num == 1) stageManager.LoadSceneByName("Main");//사진 씬으로
			else if (num == 2) stageManager.LoadSceneByName("Main");
			else if (num == 3) stageManager.QuitGame();
		}
	}

}
