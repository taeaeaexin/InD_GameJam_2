using UnityEngine;

public enum ButtonType
{
    Start,
    Option,
    Quit
}

public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private ButtonType buttonType;

    public void ButtonAction()
    {
        switch (buttonType)
        {
            case ButtonType.Start:
                GameManager.Instance.LoadScene("MainScene");
                break;
            case ButtonType.Option:
                break;
            case ButtonType.Quit:
                GameManager.Instance.QuitGame();
                break;
        }
    }
}