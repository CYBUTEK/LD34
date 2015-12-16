using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips = null;

    private AudioSource audioSource;

    private int currentTrackIndex = -1;

    public void PlayNextTrack()
    {
        PlayTrack((currentTrackIndex + 1) % clips.Length);
    }

    public void PlayTrack(int trackIndex)
    {
        if (clips != null && clips.Length > trackIndex && trackIndex >= 0 && audioSource != null)
        {
            audioSource.clip = clips[trackIndex];
            audioSource.Play();
            currentTrackIndex = trackIndex;
        }
    }

    protected virtual IEnumerator Start()
    {
        yield return new WaitUntil(() => AudioManager.IsReady);

        audioSource = AudioManager.GetAudioSource("music");
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0.0f;
        audioSource.loop = false;
    }

    protected virtual void Update()
    {
        if (audioSource != null && audioSource.isPlaying == false)
        {
            PlayNextTrack();
        }
    }
}