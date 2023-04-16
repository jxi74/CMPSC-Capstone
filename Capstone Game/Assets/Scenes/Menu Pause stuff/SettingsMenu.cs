using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour, IDataPersistence
{

    public AudioMixer audioMixer;

    //public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public float vol;

    public Slider volSlider;

    void Start()
    {
        resolutions = Screen.resolutions;

       // resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

       // resolutionDropdown.AddOptions(options);
      //  resolutionDropdown.value = currentResolutionIndex;
       // resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("volume", volume);
        this.vol = volume;
        this.volSlider.value = volume;
        AudioListener.volume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void LoadData(GameData data)
    {
        this.vol = data.volume;
        this.volSlider.value = data.volume;
        AudioListener.volume = this.vol;
    }

    public void SaveData(GameData data)
    {
        data.volume = this.vol;
    }
}
