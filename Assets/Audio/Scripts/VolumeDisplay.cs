using UnityEngine;
using UnityEngine.UI;

public class VolumeDisplay : MonoBehaviour
{
    [SerializeField]
    private Button background = null;

    private CanvasGroupFader canvasGroupFader;

    public void OnCloseClicked()
    {
        if (canvasGroupFader != null)
        {
            canvasGroupFader.Close();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Awake()
    {
        canvasGroupFader = GetComponent<CanvasGroupFader>();

        if (background != null)
        {
            background.onClick.AddListener(OnCloseClicked);
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCloseClicked();
        }
    }
}