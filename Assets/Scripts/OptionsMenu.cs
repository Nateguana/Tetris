using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20.0f);
    }
}
