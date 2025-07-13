using UnityEngine;

namespace Throwables 
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Match : Throwable
	{
		SpriteRenderer spriteRenderer;
		public GameObject fireObject; // �̸� ��Ȱ��ȭ�� �� ������Ʈ�� ����
		public Sprite off_head;

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			if (SoundManager.Instance) SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_fire_2"));
			if (fireObject != null)
			{
				fireObject.SetActive(false); // �������ڸ��� ��Ȱ��ȭ
				Invoke(nameof(ActivateFire), 0.5f); // 0.5�� �ڿ� Ȱ��ȭ
				Torque(200f);
			}
		}
		
		void ActivateFire()
		{
			fireObject.SetActive(true);
			spriteRenderer.sprite = off_head;
		}
	}
}
