using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour, IPointerClickHandler
{
    private const float VOLUME_FADE_SPEED = 0.5f;

    [SerializeField]
    private AudioMixer audioMixer = null;

    [SerializeField]
    private string exposedVolumeName = null;

    [SerializeField]
    private Sprite volumeOffSprite = null;

    [SerializeField]
    private Sprite volumeOnSprite = null;

    private Image image;
    private bool isVolumeOn = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        SetVolume(!isVolumeOn);
    }

    protected virtual void Awake()
    {
        image = GetComponent<Image>();
    }

    private IEnumerator Fade(float from, float to, float seconds)
    {
        float progress = 0.0f;

        while (progress <= 1.0f)
        {
            progress = progress + (Time.deltaTime / seconds);
            audioMixer.SetFloat(exposedVolumeName, Mathf.Lerp(from, to, progress));
            yield return null;
        }
    }

    private void SetSprite(Sprite sprite)
    {
        if (image != null)
        {
            image.sprite = sprite;
        }
    }

    private void SetVolume(bool on)
    {
        isVolumeOn = on;

        float volume;
        if (audioMixer.GetFloat(exposedVolumeName, out volume))
        {
            StopAllCoroutines();
            if (isVolumeOn)
            {
                SetSprite(volumeOnSprite);
                StartCoroutine(Fade(volume, 0.0f, VOLUME_FADE_SPEED));
            }
            else
            {
                SetSprite(volumeOffSprite);
                StartCoroutine(Fade(volume, -80.0f, VOLUME_FADE_SPEED));
            }
        }
    }
}