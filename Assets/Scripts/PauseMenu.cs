using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public bool scrollingText = false;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
    }

    public void Init()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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
}
