using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings Instance { get; private set; }
    

    private const string MASTER_VOLUME_PARAM = "MASTER_VOLUME";
    private const string MUSIC_VOLUME_PARAM = "MUSIC_VOLUME";
    private const string AMBIENCE_VOLUME_PARAM = "AMBS_VOLUME";
    private const string SFX_VOLUME_PARAM = "SFX_VOLUME";

    [SerializeField] private AudioMixer m_Mixer;
    [SerializeField] private AudioMixerSnapshot m_DefaultSnapshot;
    [SerializeField] private AudioMixerSnapshot[] m_Snapshots;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public enum AudioState
    {
        GAMEPLAY = -1,
        PAUSE_MENU,
        UNDERWATER,
        CAVE
    }

    public float MasterVolume
    {
        get => m_MasterVolume;
        set
        {
            float v = Mathf.Clamp01(value);
            if (Mathf.Approximately(v, m_MasterVolume)) return;

            m_MasterVolume = v;
            m_Mixer.SetFloat(MASTER_VOLUME_PARAM, m_MasterVolume);       
        }
    }
    private float m_MasterVolume = 1;

    public float MusicVolume
    {
        get => m_MusicVolume;
        set
        {
            float v = Mathf.Clamp01(value);
            if (Mathf.Approximately(v, m_MusicVolume)) return;

            m_MusicVolume = v;
            m_Mixer.SetFloat(MUSIC_VOLUME_PARAM, m_MusicVolume);
        }
    }
    private float m_MusicVolume = 1;

    public float SFXVolume
    {
        get => m_SFXVolume;
        set
        {
            float v = Mathf.Clamp01(value);
            if (Mathf.Approximately(v, m_SFXVolume)) return;

            m_SFXVolume = v;
            m_Mixer.SetFloat(SFX_VOLUME_PARAM, m_SFXVolume);
        }
    }
    private float m_SFXVolume = 1;


    public float AmbienceVolume
    {
        get => m_AmbienceVolume;
        set
        {
            float v = Mathf.Clamp01(value);
            if (Mathf.Approximately(v, m_AmbienceVolume)) return;

            m_AmbienceVolume = v;
            m_Mixer.SetFloat(AMBIENCE_VOLUME_PARAM, m_AmbienceVolume);
        }
    }
    private float m_AmbienceVolume = 1;


    public float GetMasterVolume() => m_MasterVolume;
    public void SetMasterVolume (float volume)
    {
        float v = Mathf.Clamp01(volume);
        if (Mathf.Approximately(v, m_AmbienceVolume)) return;

        m_AmbienceVolume = v;
        m_Mixer.SetFloat(AMBIENCE_VOLUME_PARAM, m_AmbienceVolume);
    }

    
    public AudioState CurrentSnapshot
    {
        set
        {
            if (value == AudioState.GAMEPLAY)
                return;

            m_Snapshots[(int)value].TransitionTo(2);
        }
    }

    public void SetCurrentSnapshot(AudioState state, float duration)
    {
        if (state == AudioState.GAMEPLAY)
            m_DefaultSnapshot.TransitionTo(duration);
        else
            m_Snapshots[(int)state].TransitionTo(duration);
    }

    AudioSource source;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            ActivateLoopSound();
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            DeactivateLoopSound();
    }

    private void ActivateLoopSound()
    {
        source.Play();
        // Corroutine to go up in volume/pitch
    }

    private void DeactivateLoopSound()
    {
        // Corroutine to go down in volume/pitch
        //Wait to end
        source.Stop();
    }
}
