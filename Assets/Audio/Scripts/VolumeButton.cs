using UnityEngine;
using UnityEngine.EventSystems;

public class VolumeButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.CreateVolumeDisplay();
        }
    }
}