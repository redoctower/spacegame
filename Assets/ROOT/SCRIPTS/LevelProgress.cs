using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class LevelProgress : MonoBehaviour
{
    public static float levelTime = 30f;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;
    public static int gameScore = 10;
    public UnityEvent OnStartGame;
    public UnityEvent OnEndGame;
    public static bool pause = false;

    private void Start()
    {
        OnStartGame.Invoke();
    }

    private void Update()
    {
        if (pause == true) return;
        if(levelTime <= 0 || gameScore <= 0)
            OnEndGame.Invoke();
        levelTime -= Time.deltaTime;
        timeText.text = "TIME: " + Math.Round(levelTime, 1);
        scoreText.text = "SCORE: " + gameScore;
    }

    public void EndGame()
    {
        pause = true;
        levelTime = 30;
        gameScore = 10;
    }

    public void StartGame()
    {
        pause = false;
    }
}
