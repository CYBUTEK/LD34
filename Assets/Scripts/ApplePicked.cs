using UnityEngine;

public class ApplePicked : MonoBehaviour
{
    private Apple apple;

    protected virtual void Awake()
    {
        apple = GetComponentInParent<Apple>();
    }

    protected virtual void OnMouseUpAsButton()
    {
        if (apple != null)
        {
            GameController.Instance.Points += apple.GetPoints();
            Destroy(transform.root.gameObject);
        }
    }
}