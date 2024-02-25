using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFInalScore()
    {
        scoreText.text = "测试结束\n你的得分是: " + scoreKeeper.CalculateScore() + "%";
        // Debug.Log("Correct Answer (End Screen): " + scoreKeeper.GetCorrectAnswers());
        // Debug.Log("Questions Seen (End Screen): " + scoreKeeper.GetQuestionsSeen());
        // Debug.Log("Score (End Screen): " + scoreKeeper.CalculateScore());
    }


}
