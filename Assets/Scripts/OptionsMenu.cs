using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYAxis;
    public Slider sliderInstance;
    public float sliderValue = 100f;
    private float sliderValueBeforeChange;
    public AudioMixerGroup BGM;
    private float sliderValueIndb = 0;

    // Awake is called before the Start method
    void Awake()
    {
        if (PlayerPrefs.HasKey("InvertYToggle"))
            invertYAxis.isOn = PlayerPrefs.GetInt("InvertYToggle") == 0 ? false : true;
        sliderInstance.minValue = 0;
        sliderInstance.maxValue = 100;
        sliderInstance.wholeNumbers = true;

        if (PlayerPrefs.HasKey(sliderInstance.name))
        {
            sliderInstance.value = PlayerPrefs.GetFloat(sliderInstance.name);
        }
        else
        {
            PlayerPrefs.SetFloat(sliderInstance.name, sliderValue);
        }
        sliderValueBeforeChange = sliderInstance.value;
    }
    // SFX and BGM Wrong set up
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
        // Reset Options to previous state
        sliderValueBeforeChange = Mathf.Log10(sliderValueBeforeChange) / 20.0f;
        BGM.audioMixer.SetFloat("BGMVol", (sliderValueBeforeChange));
        //BGM.audioMixer.SetFloat("SFX", (sliderValueBeforeChange));
    }

    // Apply button method
    public void Apply()
    {
        if (invertYAxis.isOn)
            PlayerPrefs.SetInt("InvertYToggle", 1);
        else
            PlayerPrefs.SetInt("InvertYToggle", 0);
        SceneManager.LoadScene("MainMenu");
        sliderValueBeforeChange = sliderInstance.value;
        PlayerPrefs.SetFloat(sliderInstance.name, sliderValue);
    }

    public void OnValueChangeBGM()
    {
        sliderValue = sliderInstance.value;
        sliderValueIndb = Mathf.Log10(sliderValue) / 20.0f;
        BGM.audioMixer.SetFloat("BGMVol", (sliderValueIndb));
    }
}
