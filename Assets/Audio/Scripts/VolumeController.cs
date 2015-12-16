using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private string parameter = null;

    private Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    protected virtual IEnumerator Start()
    {
        yield return new WaitUntil(() => AudioManager.IsReady);

        InitSliderPosition();
        AddSliderChangedListener();
    }

    private void AddSliderChangedListener()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void InitSliderPosition()
    {
        if (AudioManager.Instance != null && slider != null)
        {
            slider.value = AudioManager.Instance.GetVolume(parameter);
        }
    }

    private void OnVolumeChanged(float linearVolume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(parameter, linearVolume);
        }
    }

    private void SetSliderValue(float value)
    {
        if (slider != null)
        {
            slider.value = value;
        }
    }
}