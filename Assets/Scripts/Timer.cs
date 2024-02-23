using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isAnweringQuestion = false;
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; // Subtract the time that has passed since the last frame
        
        if (timerValue <= 0 && isAnweringQuestion)
        {
            timerValue = timeToShowCorrectAnswer;
            isAnweringQuestion = false;
        }
        else if (timerValue <= 0 && !isAnweringQuestion)
        {
            timerValue = timeToCompleteQuestion;
            isAnweringQuestion = true;
        }

        Debug.Log("Timer: " + timerValue);
    }
}
