using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer myMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;

    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Start()
    {
       
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.75f);
        ambienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 0.75f);

       
        SetMasterVolume();
        SetMusicVolume();
        SetSfxVolume();
        SetAmbienceVolume();

        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    
    public void SetMasterVolume() => SetMixerFloat("MASTER_VOLUME_PARAM", masterSlider, "MasterVolume");
    public void SetMusicVolume() => SetMixerFloat("MUSIC_VOLUME_PARAM", musicSlider, "MusicVolume");
    public void SetSfxVolume() => SetMixerFloat("SFX_VOLUME_PARAM", sfxSlider, "SfxVolume");
    public void SetAmbienceVolume() => SetMixerFloat("AMBIENCE_VOLUME_PARAM", ambienceSlider, "AmbienceVolume");

    private void SetMixerFloat(string name, Slider slider, string saveKey)
    {
        float volume = slider.value;
        myMixer.SetFloat(name, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(saveKey, volume);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}