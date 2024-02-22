using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText; // Use TextMeshProUGUI for UI TextMeshPro components
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Color32 defaultAnswerColor = new Color32(2, 138, 152, 60);
    [SerializeField] Color32 correctAnswerColor = new Color32(138, 0, 0, 60);

    void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswers(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        if (index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "回答正确!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.color = correctAnswerColor;
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            string correctAnswer = question.GetAnswers(question.GetCorrectAnswerIndex());
            questionText.text = "回答错误，正确答案是:\n" + correctAnswer;
            Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.color = correctAnswerColor;
        }
    }


}
