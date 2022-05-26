using UnityEngine;

public class GameManager : TouchMove
{
    public static GameManager instance;

    private bool canFollow = true;
    private bool canRotateCylinder = true;
    private bool isGameStarted = false;
    private bool isGameFailed = false;

    private int _score;

    public delegate void OnGameStart();
    public delegate void OnGameStop();
    public delegate void OnGameFinished();
    public delegate void OnScoreChanged(int score);

    public event OnGameStart onGameStart;
    public event OnGameStop onGameFailed;
    public event OnGameFinished onGameFinished;
    public event OnScoreChanged onScoreChanged;


    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
    }

    protected override void OnTouchMoved(Touch touch)
    {
        if (isGameStarted) return;

        isGameStarted = true;

        onGameStart.Invoke();
    }

    public void GameFailed()
    {
        onGameFailed.Invoke();

        StopTime();

        canRotateCylinder = false;
        isGameFailed = true;
    }

    public void GameFinished()
    {
        onGameFinished.Invoke();

        StopTime();
    }

    public void AddScore()
    {
        _score += 10;

        onScoreChanged.Invoke(_score);
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    }

    public bool CanPlayGame()
    {
        return canRotateCylinder && !isGameFailed;
    }

    public void SetCanRotateCylinder(bool value)
    {
        canRotateCylinder = value;
    }

    public bool GetCanFollow()
    {
        return canFollow;
    }

    public void SetCanFollow(bool value)
    {
        canFollow = value;
    }
}
