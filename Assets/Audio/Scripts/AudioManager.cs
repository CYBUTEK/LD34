using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    private static Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    [SerializeField]
    private AudioMixer mixer = null;

    [SerializeField]
    private AudioMixerGroup[] groups = null;

    [SerializeField]
    private GameObject volumeButtonPrefab = null;

    [SerializeField]
    private GameObject volumeDisplayPrefab = null;

    [SerializeField]
    private Transform canvasTransform = null;

    public static bool IsReady { get; private set; }

    public GameObject VolumeDisplay { get; private set; }

    public static AudioSource GetAudioSource(string audioMixerName)
    {
        if (string.IsNullOrEmpty(audioMixerName) == false)
        {
            AudioSource audioSource;
            if (audioSources.TryGetValue(audioMixerName.ToLowerInvariant(), out audioSource))
            {
                return audioSource;
            }
        }
        return null;
    }

    public static void PlayClip(string audioMixerGroupName, AudioClip clip, float pitch = 1.0f)
    {
        AudioSource audioSource = GetAudioSource(audioMixerGroupName);
        if (audioSource != null && clip != null)
        {
            //audioSource.clip = clip;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip);
        }
    }

    public void CreateVolumeDisplay()
    {
        if (volumeDisplayPrefab != null && canvasTransform != null && VolumeDisplay == null)
        {
            GameObject volumeDisplayObject = Instantiate(volumeDisplayPrefab);
            if (volumeDisplayObject != null)
            {
                volumeDisplayObject.transform.SetParent(canvasTransform, false);
                VolumeDisplay = volumeDisplayObject;
            }
        }
    }

    public float GetVolume(string parameter)
    {
        float volume = 0.0f;

        if (string.IsNullOrEmpty(parameter) == false && mixer != null && mixer.GetFloat(parameter, out volume))
        {
            volume = AudioUtility.LogToLinear(volume);
        }

        return volume;
    }

    public void SetVolume(string parameter, float linearVolume)
    {
        if (string.IsNullOrEmpty(parameter) == false && mixer != null)
        {
            if (mixer.SetFloat(parameter, AudioUtility.LinearToLog(linearVolume)))
            {
                PlayerPrefs.SetFloat(parameter, linearVolume);
                PlayerPrefs.Save();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);

            CreateAudioSources();
            CreateVolumeButton();
        }
    }

    protected virtual void Start()
    {
        if (mixer != null && groups != null)
        {
            for (int i = 0; i < groups.Length; ++i)
            {
                AudioMixerGroup audioMixerGroup = groups[i];
                if (audioMixerGroup != null)
                {
                    string volumeParameter = audioMixerGroup.name + "-Volume";
                    SetVolume(volumeParameter, PlayerPrefs.GetFloat(volumeParameter, GetVolume(volumeParameter)));
                }
            }
        }

        IsReady = true;
    }

    private void CreateAudioSources()
    {
        if (groups != null)
        {
            for (int i = 0; i < groups.Length; ++i)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.outputAudioMixerGroup = groups[i];
                    audioSource.spatialBlend = 0.0f;

                    audioSources.Add(audioSource.outputAudioMixerGroup.name.ToLowerInvariant(), audioSource);
                }
            }
        }
    }

    private void CreateVolumeButton()
    {
        if (volumeButtonPrefab != null && canvasTransform != null)
        {
            GameObject volumeButtonObject = Instantiate(volumeButtonPrefab);
            if (volumeButtonObject != null)
            {
                volumeButtonObject.transform.SetParent(canvasTransform, false);
            }
        }
    }
}