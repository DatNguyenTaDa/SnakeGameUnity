using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text highScore;

    
    private void Update()
    {
        score.text = "Your Score: " + MainGame.GetScore().ToString();
        highScore.text = "Your High Score: " + MainGame.GetHighScore().ToString();
    }
}
