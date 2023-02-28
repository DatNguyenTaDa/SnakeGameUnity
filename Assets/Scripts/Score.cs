using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   private Text scoreText;
   private Text highScore;

    private void Awake()
    {
        scoreText = transform.Find("score").GetComponent<Text>();
        highScore = transform.Find("highScore").GetComponent<Text>();
    }
    private void Update()
    {
        scoreText.text = "Your Score: " + MainGame.GetScore().ToString();
        highScore.text = "Your High Score: " + MainGame.GetHighScore().ToString();
    }
}
