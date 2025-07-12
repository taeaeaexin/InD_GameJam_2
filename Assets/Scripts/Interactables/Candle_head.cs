using UnityEngine;

public class Candle_head : MonoBehaviour
{
	private Match_fire matchFire;

	void Start()
	{
		matchFire = GetComponent<Match_fire>();
		if (matchFire != null)
		{
			matchFire.enabled = false; // 시작하자마자 꺼둠
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Firewood") && matchFire != null)
		{
			if (SoundManager.Instance) SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_fire_3"));
			matchFire.enabled = true; // 불 붙임
		}
	}
}
