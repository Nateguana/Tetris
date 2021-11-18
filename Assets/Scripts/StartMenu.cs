using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
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
        FindObjectOfType<AudioManager>().Play("ClickPlaceholder");
    }

        public void ClickSound2 ()
    {
        FindObjectOfType<AudioManager>().Play("Click2Placeholder");
    }

        public void ClickSound3 ()
    {
        FindObjectOfType<AudioManager>().Play("Click3Placeholder");
    }
}
