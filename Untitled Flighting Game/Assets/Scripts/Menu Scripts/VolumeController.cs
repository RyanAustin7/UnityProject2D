using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this namespace for TextMesh Pro

public class VolumeController : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    // TMP Text fields to display RTPC values
    public TMP_Text masterVolumeText;
    public TMP_Text musicVolumeText;
    public TMP_Text sfxVolumeText;

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    void Start()
    {
        // Load stored values or set defaults
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 1f); // Default 1.0
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);  // Default 1.0
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);    // Default 1.0

        // Set sliders to loaded values
        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        // Set TMP texts to initial values
        UpdateVolumeTexts();

        // Add listeners to sliders
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void UpdateVolumeTexts()
    {
        masterVolumeText.text = (masterSlider.value * 100f).ToString("F0");
        musicVolumeText.text = (musicSlider.value * 100f).ToString("F0");
        sfxVolumeText.text = (sfxSlider.value * 100f).ToString("F0");
    }

    public void SetMasterVolume(float value)
    {
        Debug.Log("Changed Master volume to: " + value);
        AkSoundEngine.SetRTPCValue("MasterVolume", value * 100f); // Adjust scaling as needed

        // Save the value to PlayerPrefs
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
        UpdateVolumeTexts(); // Update the TMP text
    }

    public void SetMusicVolume(float value)
    {
        Debug.Log("Changed Music volume to: " + value);
        AkSoundEngine.SetRTPCValue("MusicVolume", value * 100f); // Adjust scaling as needed

        // Save the value to PlayerPrefs
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        UpdateVolumeTexts(); // Update the TMP text
    }

    public void SetSFXVolume(float value)
    {
        Debug.Log("Changed SFX volume to: " + value);
        AkSoundEngine.SetRTPCValue("SFXVolume", value * 100f); // Adjust scaling as needed

        // Save the value to PlayerPrefs
        PlayerPrefs.SetFloat(SFXVolumeKey, value);
        UpdateVolumeTexts(); // Update the TMP text
    }

    private void OnDestroy()
    {
        // Ensure to save values when the script is destroyed
        PlayerPrefs.Save();
    }
}
