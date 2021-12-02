using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject resumeButton;
    public GameObject menuButton;
    public GameObject optionsButton;
    public GameObject creditsButton;
    public GameObject versionNotesButton;
    public bool scrollingText = false;

    private int numOfButtons = 5;
    private int selectedButton;
    private bool previousAxisState;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!optionsMenuUI.activeSelf && (Input.GetKeyDown(KeyCode.P) || 
            (Input.GetJoystickNames().Any() && Input.GetKeyDown(KeyCode.JoystickButton7))))
        {
            if (scrollingText)
            {
                LoadMenu();
                AudioManager.Play("Click2Placeholder"); // Placeholder
            }
            else if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || GetNegAxisDown("Vertical"))
            {
                selectedButton += 1;
                if (selectedButton > numOfButtons)
                {
                    selectedButton = 1;
                }

                EventSystem.current.SetSelectedGameObject(null);

                switch (selectedButton)
                {
                    case 1:
                        EventSystem.current.SetSelectedGameObject(resumeButton);
                        break;
                    case 2:
                        EventSystem.current.SetSelectedGameObject(menuButton);
                        break;
                    case 3:
                        EventSystem.current.SetSelectedGameObject(optionsButton);
                        break;
                    case 4:
                        EventSystem.current.SetSelectedGameObject(creditsButton);
                        break;
                    case 5:
                        EventSystem.current.SetSelectedGameObject(versionNotesButton);
                        break;
                }
            }
        }
    }

    public void Init()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        previousAxisState = false;
    }

    public void Resume()
    {
        Init();
         AudioManager.Play("ClickPlaceholder"); // Placeholder
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
        selectedButton = 1;
        AudioManager.Play("Click2Placeholder"); // Placeholder
    }

    public void LoadMenu()
    {
        Init();
        SceneManager.LoadScene(1);
        AudioManager.Play("Click2Placeholder"); // Placeholder
    }

    public void CreditsScreen()
    {
        Init();
        SceneManager.LoadScene(3);
        AudioManager.Play("Click2Placeholder"); // Placeholder
    }

    public void VersionNotesScreen()
    {
        Init();
        SceneManager.LoadScene(4);
        AudioManager.Play("Click3Placeholder"); // Placeholder
    }

    private bool GetNegAxisDown(string axisName)
    {
        bool isAxisPressed = Input.GetAxis(axisName) < 0;

        if (isAxisPressed && previousAxisState)
        {
            return false;
        }

        previousAxisState = isAxisPressed;

        return isAxisPressed;
    }
}
