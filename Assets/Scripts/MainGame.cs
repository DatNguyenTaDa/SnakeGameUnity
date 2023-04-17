using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] private Snake snake;
    private static MainGame instance;
    private static int score;
    private static int highScore;

    private LevelSnake levelGrid;

    private void Awake()
    {
        
        instance = this;
    }
    private void Start()
    {
        Debug.Log("Game Start");
        score= 0;
        Audio.instance.Theme();
        highScore = PlayerPrefs.GetInt("snakeHighScore", 0);
        levelGrid = new LevelSnake(9, 6);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }
    private void Update()
    {
        if(View.soundTheme == 0)
        {
            Audio.instance.Mute();
        }
        else Audio.instance.DontMute();
    }
    public static int GetScore()
    {
        return score;
    }
    public static void AddScore()
    {
        score += 10;
    }
    public static int GetHighScore()
    {
        if (score > highScore)
            PlayerPrefs.SetInt("snakeHighScore", score);

        return PlayerPrefs.GetInt("snakeHighScore", score);
    }
}
