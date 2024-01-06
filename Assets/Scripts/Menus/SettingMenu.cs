using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public InputActionAsset actionAsset;

    public Slider VolumeSlider;
    public Slider SensitivitySlider;

    public TMP_Dropdown difficultyDropdown;
    public TMP_Dropdown GraphicsDropdown;

    private void Start()
    {
        if (PlayerPrefs.HasKey("InputBindings"))
            actionAsset.LoadFromJson(PlayerPrefs.GetString("InputBindings"));

        if (PlayerPrefs.HasKey("volume"))
            VolumeSlider.value = PlayerPrefs.GetFloat("volume");

        if(PlayerPrefs.HasKey("MouseSensitivity"))
            SensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");

        if (PlayerPrefs.HasKey("Difficulty"))
            difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty");

        if (PlayerPrefs.HasKey("HighSettings"))
            GraphicsDropdown.value = PlayerPrefs.GetInt("HighSettings");
    }

    public void volumeSet (float volume){
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void setCameraSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    public void difficultySet(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }

    public void qualitySet (int quality){
        //QualitySettings.SetQualityLevel(quality);
        PlayerPrefs.SetInt("HighSettings", quality);
    }

    private void OnDisable()
    {
        print(actionAsset.ToJson());
        // save input bindings
        // disable for now causing issues
        //PlayerPrefs.SetString("InputBindings", actionAsset.ToJson());
    }
}
