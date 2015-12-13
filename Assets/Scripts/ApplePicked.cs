using UnityEngine;

public class ApplePicked : MonoBehaviour
{
    [SerializeField]
    private AudioClip popClip = null;

    private Apple apple;

    protected virtual void Awake()
    {
        apple = GetComponentInParent<Apple>();
    }

    protected virtual void OnMouseDown()
    {
        if (apple != null)
        {
            apple.IsPicked = true;
            GameController.Instance.Points += apple.GetPoints();
            AudioManager.PlayClip("effects", popClip, Random.Range(0.8f, 1.2f));
            Destroy(transform.root.gameObject);
        }
    }
}