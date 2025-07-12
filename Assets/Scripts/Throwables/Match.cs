using UnityEngine;

namespace Throwables 
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Match : Throwable
	{
		SpriteRenderer spriteRenderer;
		public GameObject fireObject; // 미리 비활성화된 불 오브젝트를 지정
		public Sprite off_head;

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			if (fireObject != null)
			{
				fireObject.SetActive(false); // 시작하자마자 비활성화
				Invoke(nameof(ActivateFire), 0.5f); // 0.5초 뒤에 활성화
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
