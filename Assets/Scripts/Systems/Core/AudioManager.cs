using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Effects")]
    [SerializeField] AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] soundEffects;
    [SerializeField] AudioClip[] musicTracks;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(string clipName)
    {
        AudioClip clip = GetClipByName(clipName, soundEffects);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect {clipName} not found!");
        }
    }

    public void PlayMusic(string trackName)
    {
        AudioClip track = GetClipByName(trackName, musicTracks);
        if (track != null)
        {
            musicSource.clip = track;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music track {trackName} not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    private AudioClip GetClipByName(string name, AudioClip[] clips)
    {
        foreach (var clip in clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
    public void PlayRandomMusic()
    {
        if (musicTracks.Length == 0)
        {
            Debug.LogWarning("No music tracks available!");
            return;
        }

        int randomIndex = Random.Range(0, musicTracks.Length);

        musicSource.clip = musicTracks[randomIndex];

        musicSource.Play();
    }
}
