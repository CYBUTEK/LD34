using UnityEngine;

public class AppleDropped : MonoBehaviour
{
    private Apple apple;

    protected virtual void Awake()
    {
        apple = GetComponentInParent<Apple>();
    }

    protected virtual void OnBecameInvisible()
    {
        if (apple != null && apple.IsPicked == false && GameController.IsPlaying)
        {
            GameController.Instance.Lives -= 1;
            Destroy(transform.root.gameObject);
        }
    }
}