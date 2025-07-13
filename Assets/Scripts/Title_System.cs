using UnityEngine;

public class Title_System : MonoBehaviour
{
	public GameObject hand;

    private void Start()
    {
        re_obj();
    }

    public void re_obj()
    {
        Destroy(hand);
		Instantiate(hand);
    }
}
