using UnityEngine;

public class AppleDropped : MonoBehaviour
{
    protected virtual void OnBecameInvisible()
    {
        GameController.Instance.Lives -= 1;

        Destroy(transform.root.gameObject);
    }
}