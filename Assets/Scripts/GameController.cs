using UnityEngine;
using UnityEngine.UI;

public class GameController : SingletonBehaviour<GameController>
{
    [SerializeField]
    private Text pointsText = null;

    [SerializeField]
    private Text livesText = null;

    private int lives;

    private int points;

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            if (value < 0)
            {
                lives = 0;
                OnGameOver();
            }
            else
            {
                lives = value;
            }

            if (livesText != null)
            {
                livesText.text = "LIVES: " + lives;
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
            if (value < 0)
            {
                points = 0;
                OnGameOver();
            }
            else
            {
                points = value;
            }

            if (pointsText != null)
            {
                pointsText.text = "POINTS: " + points;
            }
        }
    }

    public void OnGameOver() { }

    public void StartGame()
    {
        Lives = 3;
        Points = 0;
    }

    protected virtual void Start()
    {
        StartGame();
    }
}