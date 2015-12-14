using UnityEngine;

public class ApplePicked : MonoBehaviour
{
    private const float PICKED_FORCE = 5.0f;

    [SerializeField]
    private AudioClip popClip = null;

    private Apple apple;
    private Vector2 leftForceVector = new Vector2(-1.0f, 1.0f);
    private Vector2 rightForceVector = new Vector2(1.0f, 1.0f);

    protected virtual void Awake()
    {
        apple = GetComponent<Apple>();
    }

    protected virtual void OnMouseDown()
    {
        if (apple != null && apple.IsPicked == false && apple.IsDropped == false)
        {
            apple.IsPicked = true;
            GameController.Instance.Points += apple.GetPoints();
            AudioManager.PlayClip("effects", popClip, Random.Range(0.8f, 1.2f));

            if (apple.HasWorm)
            {
                GameController.Instance.Lives += 1;
            }

            if (apple.Rigidbody2D != null)
            {
                apple.Rigidbody2D.isKinematic = false;

                if (transform.position.x < 0.0f)
                {
                    apple.Rigidbody2D.AddForce(leftForceVector * PICKED_FORCE, ForceMode2D.Impulse);
                }
                else
                {
                    apple.Rigidbody2D.AddForce(rightForceVector * PICKED_FORCE, ForceMode2D.Impulse);
                }
            }
        }
    }
}