using UnityEngine;

public class Candle_head : MonoBehaviour
{
	private Match_fire matchFire;

	void Start()
	{
		matchFire = GetComponent<Match_fire>();
		if (matchFire != null)
		{
			matchFire.enabled = false; // �������ڸ��� ����
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Firewood") && matchFire != null)
		{
			if (SoundManager.Instance) SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_fire_3"));
			matchFire.enabled = true; // �� ����
		}
	}
}
