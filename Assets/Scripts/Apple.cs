﻿using UnityEngine;

public class Apple : MonoBehaviour
{
    private const float RANDOM_WORM_PERCENTAGE = 0.1f;

    [SerializeField]
    private SpriteRenderer bodyRenderer = null;

    [SerializeField]
    private Gradient bodyGradient = null;

    [SerializeField]
    private AnimationCurve ripenessCurve = null;

    [SerializeField]
    private GameObject wormObject = null;

    private float elapsedTime;
    private Vector3 originalScale;
    private float timeTillRipeSeconds = 2.5f;

    public bool IsDropped { get; set; }

    public bool IsPicked { get; set; }

    public Rigidbody2D Rigidbody2D { get; set; }

    public SpawnNode SpawnNode { get; set; }

    public bool HasWorm
    {
        get
        {
            if (wormObject != null)
            {
                return wormObject.activeSelf;
            }
            return false;
        }
    }

    public int GetPoints()
    {
        if (ripenessCurve != null)
        {
            return Mathf.RoundToInt(ripenessCurve.Evaluate(elapsedTime) * 10.0f);
        }
        return 0;
    }

    protected virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.Range(-30.0f, 30.0f));

        originalScale = transform.localScale;

        timeTillRipeSeconds = Random.Range(2.0f, 5.0f);

        SetColour(0.0f);
        SetScale(0.0f);

        if (wormObject != null)
        {
            wormObject.SetActive(Random.value < RANDOM_WORM_PERCENTAGE);
        }

        AppleSpawner.Apples.Add(this);
    }

    protected virtual void OnDestroy()
    {
        AppleSpawner.Apples.Remove(this);

        if (SpawnNode != null)
        {
            SpawnNode.IsOccupied = false;
        }
    }

    protected virtual void Start()
    {
        if (SpawnNode != null)
        {
            SpawnNode.IsOccupied = true;
        }
    }

    protected virtual void Update()
    {
        elapsedTime = elapsedTime + (Time.deltaTime / timeTillRipeSeconds);

        if (elapsedTime >= 1.0f && Rigidbody2D != null && IsPicked == false)
        {
            IsDropped = true;

            Rigidbody2D.isKinematic = false;
            Rigidbody2D.AddTorque(Random.Range(-1.0f, 1.0f), ForceMode2D.Impulse);

            if (SpawnNode != null)
            {
                SpawnNode.IsOccupied = false;
            }
            enabled = false;
        }

        if (IsPicked == false)
        {
            SetColour(elapsedTime);
            SetScale(elapsedTime);
        }
    }

    private void SetColour(float progress)
    {
        if (bodyRenderer != null && bodyGradient != null)
        {
            bodyRenderer.color = bodyGradient.Evaluate(progress);
        }
    }

    private void SetScale(float progress)
    {
        transform.localScale = originalScale * Mathf.Clamp01(progress);
    }
}