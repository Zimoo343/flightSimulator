using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text scoreText;

    private int score = 0; 
    
    void Start()
    {
        UpdateScoreText();
    }

    public void IncrementScore(){
        score++;
        UpdateScoreText();
    }
    
    public void UpdateScoreText(){
        scoreText.text = "Score: " + score.ToString() + "/20";
    }
}
