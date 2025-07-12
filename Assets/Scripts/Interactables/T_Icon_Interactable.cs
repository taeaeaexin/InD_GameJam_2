using Interactables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_Icon_Interactable : Interactable
{
    public override void Interact(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Failed"))
            SceneManager.LoadScene("Title");
    }
}
