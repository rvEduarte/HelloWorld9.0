using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class KurtSettingManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterVol; 
    public Slider musicVol;  
    public Slider sfxVol;    

    [Header("Audio Mixer")]
    public AudioMixer settingAudioMixer; 

    [Header("Music Volume Icons")]
    public Image musicIcon;             
    public Sprite musicVolumeSprite;    
    public Sprite musicMutedSprite;     

    [Header("Master Volume Icons")]
    public Image masterIcon;           
    public Sprite masterVolumeSprite;   
    public Sprite masterMutedSprite;    

    [Header("Sound Effects Icons")]
    public Image sfxIcon;               
    public Sprite sfxVolumeSprite;      
    public Sprite sfxMutedSprite;      

    private bool muted = false;

    public void ChangeMasterVolume()
    {
        settingAudioMixer.SetFloat("MasterVol", masterVol.value);

        // Change the sprite based on the master volume value
        if (masterVol.value <= masterVol.minValue + 0.01f) 
        {
            masterIcon.sprite = masterMutedSprite; 
        }
        else
        {
            masterIcon.sprite = masterVolumeSprite; 
        }
    }

    public void ChangeMusicVolume()
    {
        settingAudioMixer.SetFloat("MusicV", musicVol.value);

        // Change the sprite based on the music volume value
        if (musicVol.value <= musicVol.minValue + 0.01f) 
        {
            musicIcon.sprite = musicMutedSprite; 
        }
        else
        {
            musicIcon.sprite = musicVolumeSprite; 
        }
    }

    public void ChangeSFXVolume()
    {
        settingAudioMixer.SetFloat("MyExposedParam 2", sfxVol.value);

        // Change the sprite based on the SFX volume value
        if (sfxVol.value <= sfxVol.minValue + 0.01f) 
        {
            sfxIcon.sprite = sfxMutedSprite; 
        }
        else
        {
            sfxIcon.sprite = sfxVolumeSprite; 
        }
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void OnButtonPress()
    {
        muted = !muted;
        AudioListener.pause = muted;
        Save();
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        Load();
        AudioListener.pause = muted;

        // Update icons based on initial slider values
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    void Update()
    {
        // Empty for now
    }
}
