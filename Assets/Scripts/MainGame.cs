using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] private Snake snake;
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip src1, src2;
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
        src.clip = src1;
        src.Play();
        score= 0;
        levelGrid = new LevelSnake(24, 18);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
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
        if(score>highScore)
            highScore = score;
        return highScore;
    }
}
