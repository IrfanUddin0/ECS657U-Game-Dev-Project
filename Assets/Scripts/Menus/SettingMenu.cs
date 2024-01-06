using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
   public void volumeSet (float volume){

        audioMixer.SetFloat("volume", volume);

   }

   public void qualitySet (int quality){

        //QualitySettings.SetQualityLevel(quality);
        PlayerPrefs.SetInt("HighSettings", quality);

   }
}
