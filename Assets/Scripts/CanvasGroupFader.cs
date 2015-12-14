using System;
using System.Collections;
using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    private const float FADE_DURATION_SECONDS = 0.5f;

    private CanvasGroup canvasGroup;

    public bool IsFaded
    {
        get
        {
            if (canvasGroup != null)
            {
                return (canvasGroup.alpha < 1.0f);
            }
            return false;
        }
    }

    public bool IsVisible
    {
        get
        {
            if (canvasGroup != null)
            {
                return (canvasGroup.alpha > 0.0f);
            }
            return gameObject.activeSelf;
        }
    }

    public void Hide(Action callback = null)
    {
        if (canvasGroup != null)
        {
            StartCoroutine(Fade(canvasGroup.alpha, 0.0f, FADE_DURATION_SECONDS, () =>
            {
                gameObject.SetActive(false);

                if (callback != null)
                {
                    callback.Invoke();
                }
            }));
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(Fade(0.0f, 1.0f, FADE_DURATION_SECONDS, null));
    }

    protected virtual void Update()
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = (IsFaded == false);
        }
    }

    private IEnumerator Fade(float from, float to, float seconds, Action callback)
    {
        if (canvasGroup != null)
        {
            float progress = 0.0f;

            while (progress <= 1.0f)
            {
                progress = progress + (Time.deltaTime / seconds);
                canvasGroup.alpha = Mathf.Lerp(from, to, progress);
                yield return null;
            }

            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }
}