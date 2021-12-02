using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    void Start()
    {
        float volume;
        bool result = audioMixer.GetFloat("Volume", out volume);
        slider.value = Mathf.Pow(10, volume) / 20.0f;
    }

    public GameObject volumeSlider;
    public GameObject backButton;

    private int numOfButtons = 2;
    private int selectedButton;
    private bool previousAxisState;

    void Start ()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(volumeSlider);
        selectedButton = 1;
        previousAxisState = false;
    }

    void Update ()
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
                    EventSystem.current.SetSelectedGameObject(volumeSlider);
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(backButton);
                    break;
            }
        }
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20.0f);
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
