using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UserInterfaceScreens
{
    MainMenu,
    PauseMenu,
    EndMenu,
    None
}

public class UIManager : MonoBehaviour
{
    public GameManager GM;
    public UserInterfaceScreens StartScreen;
    public GameObject MainPanel;
    public GameObject PausePanel;
    public GameObject EndPanel;
    public Text EndText;


    private UserInterfaceScreens currentScreen;
    public UserInterfaceScreens Screen
    {
        get
        {
            return currentScreen;
        }
        set
        {
            setPanel(currentScreen, false);
            setPanel(value, true);
            currentScreen = value;
        }
    }

	void Start()
    {
        currentScreen = StartScreen;
        setPanel(currentScreen, true);

	}

    public void SetEndText(string s)
    {
        EndText.text = s;
    }

    private void setPanel(UserInterfaceScreens screen, bool active)
    {

        switch (screen)
        {
            case UserInterfaceScreens.MainMenu:
                MainPanel.SetActive(active);
                break;
            case UserInterfaceScreens.PauseMenu:
                PausePanel.SetActive(active);
                break;
            case UserInterfaceScreens.EndMenu:
                EndPanel.SetActive(active);
                break;
            default:
                break;
        }
    }

    public void StartButtonClicked()
    {
        GM.InitializeGame();
    }

    public void QuitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ResumeButtonClicked()
    {
        GM.Resume();
    }

    public void ReturnToMain()
    {
        GM.InitializeMenu();
        Screen = UserInterfaceScreens.MainMenu;
    }

    public void RetryClicked()
    {
        GM.InitializeGame();
    }
}
