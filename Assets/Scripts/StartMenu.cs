using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CreditsScreen ()
    {
        SceneManager.LoadScene(2);
    }

    public void VersionNotesScreen ()
    {
        SceneManager.LoadScene(3);
    }

    public void OriginalGame ()
    {
        SceneManager.LoadScene(4);
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
