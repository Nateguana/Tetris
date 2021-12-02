using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject creditsButton;
    public GameObject versionNotesButton;
    public GameObject originalGameButton;

    private int numOfButtons = 5;
    private int selectedButton;
    private bool previousAxisState;

    void Start ()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
        selectedButton = 1;
        previousAxisState = false;
    }

    void Update ()
    {
        if (GetNegAxisDown("Vertical"))
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
                    EventSystem.current.SetSelectedGameObject(playButton);
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(optionsButton);
                    break;
                case 3:
                    EventSystem.current.SetSelectedGameObject(creditsButton);
                    break;
                case 4:
                    EventSystem.current.SetSelectedGameObject(versionNotesButton);
                    break;
                case 5:
                    EventSystem.current.SetSelectedGameObject(originalGameButton);
                    break;
            }
        }
    }
    
    public void PlayGame ()
    {
        SceneManager.LoadScene(2);
    }

    public void CreditsScreen ()
    {
        SceneManager.LoadScene(3);
    }

    public void VersionNotesScreen ()
    {
        SceneManager.LoadScene(4);
    }

    public void OriginalGame ()
    {
        SceneManager.LoadScene(5);
    }

    public void ClickSound ()
    {
        AudioManager.Play("ClickPlaceholder");
    }

        public void ClickSound2 ()
    {
        AudioManager.Play("Click2Placeholder");
    }

        public void ClickSound3 ()
    {
        AudioManager.Play("Click3Placeholder");
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
