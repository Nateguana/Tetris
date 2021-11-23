using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("joystick button 0"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
