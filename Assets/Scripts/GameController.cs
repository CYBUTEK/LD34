using UnityEngine;
using UnityEngine.UI;

public class GameController : SingletonBehaviour<GameController>
{
    [SerializeField]
    private AppleSpawner appleSpawner = null;

    [SerializeField]
    private Text pointsText = null;

    [SerializeField]
    private Text dropsAllowedText = null;

    [SerializeField]
    private GameObject menuObject = null;

    private int lives;

    private int points;

    public static bool IsPlaying { get; private set; }

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            if (value <= 0 && IsPlaying)
            {
                lives = 0;
                OnGameOver();
            }
            else
            {
                lives = value;
            }

            if (dropsAllowedText != null)
            {
                dropsAllowedText.text = "DROPS ALLOWED: " + lives;
            }
        }
    }

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;

            if (pointsText != null)
            {
                pointsText.text = "POINTS: " + points;
            }
        }
    }

    public void OnGameOver()
    {
        IsPlaying = false;

        if (appleSpawner != null)
        {
            appleSpawner.StopSpawner();
        }

        DestroyAllApples();

        ShowMenu(true);
    }

    public void ShowMenu(bool state)
    {
        if (menuObject != null)
        {
            menuObject.SetActive(state);
        }
    }

    public void StartGame()
    {
        Lives = 3;
        Points = 0;

        if (appleSpawner != null)
        {
            appleSpawner.StartSpawner();
        }

        IsPlaying = true;
        ShowMenu(false);
    }

    protected virtual void Start()
    {
        ShowMenu(true);
    }

    private void DestroyAllApples()
    {
        for (int i = 0; i < AppleSpawner.Apples.Count; ++i)
        {
            Apple apple = AppleSpawner.Apples[i];

            if (apple != null)
            {
                apple.SpawnNode.IsOccupied = false;
                Destroy(apple.gameObject);
            }
        }
    }
}