using UnityEngine;

public class AppleDropped : MonoBehaviour
{
    [SerializeField]
    private AudioClip thudClip = null;

    private Apple apple;

    protected virtual void Awake()
    {
        apple = GetComponentInParent<Apple>();
    }

    protected virtual void OnBecameInvisible()
    {
        if (apple != null && apple.IsPicked == false && apple.IsDropped && GameController.IsPlaying)
        {
            GameController.Instance.Lives -= 1;
            AudioManager.PlayClip("effects", thudClip, Random.Range(0.8f, 1.2f));
        }

        Destroy(transform.root.gameObject);
    }
}