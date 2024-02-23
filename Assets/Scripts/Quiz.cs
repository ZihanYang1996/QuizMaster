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
        GetNextQuestion();
    }

    private void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswers(i);
        }
    }

    private void SetButtonsState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonColor()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.color = defaultAnswerColor;
        }
    }

    private void GetNextQuestion()
    {
        SetButtonsState(true); // Enable buttons for the next question
        SetDefaultButtonColor(); // Set default color for buttons
        DisplayQuestion();
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
        SetButtonsState(false); // Disable buttons after an answer is selected
    }


}
