using UnityEngine;

public class UIWorking : MonoBehaviour
{
    public GameObject IntroductionPanel;
    public PlayerNovement Movement;

    void Start()
    {
        IntroductionPanel.SetActive(true);
        Movement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        IntroductionPanel.SetActive(false);
        Movement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}