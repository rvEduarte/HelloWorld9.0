using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class KurtSettingManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterVol; // Slider for master volume
    public Slider musicVol;  // Slider for music volume
    public Slider sfxVol;    // Slider for sound effects volume

    [Header("Audio Mixer")]
    public AudioMixer settingAudioMixer; // Reference to the audio mixer

    [Header("Music Volume Icons")]
    public Image musicIcon;             // UI Image component for the music icon
    public Sprite musicVolumeSprite;    // Sprite for when there is music volume
    public Sprite musicMutedSprite;     // Sprite for when music is muted

    [Header("Master Volume Icons")]
    public Image masterIcon;            // UI Image component for the master icon
    public Sprite masterVolumeSprite;   // Sprite for when there is master volume
    public Sprite masterMutedSprite;    // Sprite for when master volume is muted

    [Header("Sound Effects Icons")]
    public Image sfxIcon;               // UI Image component for the sound effects icon
    public Sprite sfxVolumeSprite;      // Sprite for when there is sound effects volume
    public Sprite sfxMutedSprite;       // Sprite for when sound effects are muted

    private bool muted = false;

    public void ChangeMasterVolume()
    {
        settingAudioMixer.SetFloat("MasterVol", masterVol.value);

        // Change the sprite based on the master volume value
        if (masterVol.value <= masterVol.minValue + 0.01f) // Check if slider value is close to minimum
        {
            masterIcon.sprite = masterMutedSprite; // Set to muted icon
        }
        else
        {
            masterIcon.sprite = masterVolumeSprite; // Set to volume icon
        }
    }

    public void ChangeMusicVolume()
    {
        settingAudioMixer.SetFloat("MusicV", musicVol.value);

        // Change the sprite based on the music volume value
        if (musicVol.value <= musicVol.minValue + 0.01f) // Check if slider value is close to minimum
        {
            musicIcon.sprite = musicMutedSprite; // Set to muted icon
        }
        else
        {
            musicIcon.sprite = musicVolumeSprite; // Set to volume icon
        }
    }

    public void ChangeSFXVolume()
    {
        settingAudioMixer.SetFloat("MyExposedParam 2", sfxVol.value);

        // Change the sprite based on the SFX volume value
        if (sfxVol.value <= sfxVol.minValue + 0.01f) // Check if slider value is close to minimum
        {
            sfxIcon.sprite = sfxMutedSprite; // Set to muted icon
        }
        else
        {
            sfxIcon.sprite = sfxVolumeSprite; // Set to volume icon
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
        // Update code if needed
    }
}
