using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    private static Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    [SerializeField]
    private AudioMixerGroup[] mixerGroups = null;

    [SerializeField]
    private AudioClip musicClip = null;

    public static AudioSource GetAudioSource(string mixerName)
    {
        if (string.IsNullOrEmpty(mixerName) == false)
        {
            AudioSource audioSource;
            if (audioSources.TryGetValue(mixerName.ToLowerInvariant(), out audioSource))
            {
                return audioSource;
            }
        }
        return null;
    }

    public static void PlayClip(string mixerName, AudioClip clip, float pitch = 1.0f)
    {
        AudioSource audioSource = GetAudioSource(mixerName);
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);

            if (mixerGroups != null)
            {
                for (int i = 0; i < mixerGroups.Length; ++i)
                {
                    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                    if (audioSource != null)
                    {
                        audioSource.outputAudioMixerGroup = mixerGroups[i];
                        audioSource.spatialBlend = 0.0f;

                        audioSources.Add(audioSource.outputAudioMixerGroup.name.ToLowerInvariant(), audioSource);
                    }
                }
            }
        }
    }

    protected virtual void Start()
    {
        if (musicClip != null)
        {
            AudioSource audioSource = GetAudioSource("music");

            if (audioSource != null)
            {
                audioSource.loop = true;
                audioSource.clip = musicClip;
                audioSource.spatialBlend = 0.0f;
                audioSource.Play();
            }
        }
    }
}