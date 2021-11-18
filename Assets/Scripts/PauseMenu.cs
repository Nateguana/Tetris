using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public bool scrollingText = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (scrollingText)
            {
                LoadMenu();
                FindObjectOfType<AudioManager>().Play("Click2Placeholder"); // Placeholder
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
         FindObjectOfType<AudioManager>().Play("ClickPlaceholder"); // Placeholder
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<AudioManager>().Play("Click2Placeholder"); // Placeholder
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().Play("Click2Placeholder"); // Placeholder
    }

    public void CreditsScreen()
    {
        SceneManager.LoadScene(2);
        FindObjectOfType<AudioManager>().Play("Click2Placeholder"); // Placeholder
    }

    public void VersionNotesScreen()
    {
        SceneManager.LoadScene(3);
        FindObjectOfType<AudioManager>().Play("Click3Placeholder"); // Placeholder
    }
}
