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
    private Text highScoreText = null;

    [SerializeField]
    private CanvasGroupFader menuCanvasFader = null;

    private int highScore;

    private int lives;

    private int points;

    public static bool IsPlaying { get; private set; }

    public int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            if (highScore < value)
            {
                highScore = value;

                if (highScoreText != null)
                {
                    highScoreText.text = "HIGH SCORE: " + highScore;
                }
            }
        }
    }

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

        HighScore = points;

        ShowMenu();
    }

    public void ShowMenu()
    {
        if (menuCanvasFader != null)
        {
            menuCanvasFader.Show();
        }
    }

    public void StartGame()
    {
        if (menuCanvasFader != null)
        {
            menuCanvasFader.Hide(() =>
            {
                Lives = 3;
                Points = 0;

                if (appleSpawner != null)
                {
                    appleSpawner.StartSpawner();
                }

                IsPlaying = true;
            });
        }
    }

    protected virtual void Start()
    {
        ShowMenu();
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