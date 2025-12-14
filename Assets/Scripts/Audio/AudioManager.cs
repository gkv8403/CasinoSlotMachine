using UnityEngine;

/// <summary>
/// Manages all game sounds including SFX and background music
/// Single Responsibility: Audio playback
/// </summary>
public class AudioManager : MonoBehaviour
{
    

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSource;      // For sound effects
    [SerializeField] private AudioSource bgmAudioSource;      // For background music

    [Header("Sound Effects")]
    [SerializeField] private AudioClip leverPullSFX;
    [SerializeField] private AudioClip spinSFX;
    [SerializeField] private AudioClip loseSFX;
    [SerializeField] private AudioClip winSFX;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Audio Settings")]
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private float bgmVolume = 0.5f;

  
    void Start()
    {
        // Set initial volumes
        sfxAudioSource.volume = sfxVolume;
        bgmAudioSource.volume = bgmVolume;

        // Play background music
        if (backgroundMusic != null)
        {
            bgmAudioSource.clip = backgroundMusic;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
            Debug.Log("[AudioManager] Background music started");
        }

        // Subscribe to game events
        SubscribeToEvents();
    }

    void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        GameEvents.OnLeverPulled += PlayLeverPullSound;
        GameEvents.OnSpinStarted += PlaySpinSound;
        GameEvents.OnLose += PlayLoseSound;
        GameEvents.OnWin += HandleWinSound;
    }

    private void UnsubscribeFromEvents()
    {
        GameEvents.OnLeverPulled -= PlayLeverPullSound;
        GameEvents.OnSpinStarted -= PlaySpinSound;
        GameEvents.OnLose -= PlayLoseSound;
        GameEvents.OnWin -= HandleWinSound;
    }

    private void PlayLeverPullSound()
    {
        if (leverPullSFX != null)
        {
            sfxAudioSource.PlayOneShot(leverPullSFX, sfxVolume);
            Debug.Log("[AudioManager] Lever pull sound played");
        }
        else
        {
            Debug.LogWarning("[AudioManager] Lever pull SFX not assigned!");
        }
    }

    private void PlaySpinSound()
    {
        if (spinSFX != null)
        {
            sfxAudioSource.PlayOneShot(spinSFX, sfxVolume);
            Debug.Log("[AudioManager] Spin sound played");
        }
        else
        {
            Debug.LogWarning("[AudioManager] Spin SFX not assigned!");
        }
    }

    private void PlayLoseSound()
    {
        if (loseSFX != null)
        {
            sfxAudioSource.PlayOneShot(loseSFX, sfxVolume);
            Debug.Log("[AudioManager] Lose sound played");
        }
        else
        {
            Debug.LogWarning("[AudioManager] Lose SFX not assigned!");
        }
    }

    private void HandleWinSound(int winAmount)
    {
        if (winSFX != null)
        {
            sfxAudioSource.PlayOneShot(winSFX, sfxVolume);
            Debug.Log("[AudioManager] Win sound played");
        }
        else
        {
            Debug.LogWarning("[AudioManager] Win SFX not assigned!");
        }
    }

    // Public methods to control audio
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxAudioSource.volume = sfxVolume;
        Debug.Log($"[AudioManager] SFX volume set to {sfxVolume}");
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmAudioSource.volume = bgmVolume;
        Debug.Log($"[AudioManager] BGM volume set to {bgmVolume}");
    }

    public void MuteSFX(bool mute)
    {
        sfxAudioSource.mute = mute;
        Debug.Log($"[AudioManager] SFX {(mute ? "muted" : "unmuted")}");
    }

    public void MuteBGM(bool mute)
    {
        bgmAudioSource.mute = mute;
        Debug.Log($"[AudioManager] BGM {(mute ? "muted" : "unmuted")}");
    }
}