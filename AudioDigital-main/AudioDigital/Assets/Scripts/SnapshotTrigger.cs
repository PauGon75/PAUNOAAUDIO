using UnityEngine;
using UnityEngine.Audio;

public class SnapshotTrigger : MonoBehaviour
{
    [Header("Mixer Settings")]
    public AudioMixer mainMixer; // Drag your GameMixer here
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot zoneSnapshot;
    public float transitionTime = 1.0f;

    [Header("Volume Overrides (Optional)")]
    [Tooltip("Check this to force music to -80dB on entry regardless of sliders")]
    public bool muteMusicInZone = true;

    private void OnTriggerEnter(Collider other)
    {
        // Check for Player tag on object or parent
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            Debug.Log("Entering Zone: " + zoneSnapshot.name);

            // 1. Transition the Snapshot (handles effects/other groups)
            zoneSnapshot.TransitionTo(transitionTime);

            // 2. Force the Volume (This wins the fight against the Sliders)
            if (muteMusicInZone)
            {
                // Make sure "MusicVol" matches the name in your Exposed Parameters
                mainMixer.SetFloat("MUSIC_VOLUME_PARAM", -80f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            Debug.Log("Exiting Zone: Back to Normal");

            // 1. Transition back to Default
            normalSnapshot.TransitionTo(transitionTime);

            // 2. Give control back to the PlayerPrefs/Slider
            float savedVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            float dB = Mathf.Log10(savedVol) * 20;
            mainMixer.SetFloat("MUSIC_VOLUME_PARAM", dB);
        }
    }
}