using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText; // Use TextMeshProUGUI for UI TextMeshPro components
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Buttons")]
    [SerializeField] Color32 defaultAnswerColor = new Color32(2, 138, 152, 60);
    [SerializeField] Color32 correctAnswerColor = new Color32(138, 0, 0, 60);

    [Header("Timer")]
    [SerializeField] Image timerImage; // Reference to the Image component
    [SerializeField] Timer timer; // Reference to the Timer script
    // [SerializeField] GameObject timerObject; // Another way to reference the Timer script
    // Timer timerComponent; // Reference to the Timer script

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Start()
    {
        // timerComponent = timerObject.GetComponent<Timer>(); // Access the Timer script from the Timer GameObject
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        // timerImage.fillAmount = timerComponent.fillFraction; // Access the fillFraction variable from the Timer script
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false; // Reset the hasAnsweredEarly variable
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnweringQuestion)
        {
            DisplayAnswer(-1); // Treate the answer as incorrect if the player didn't answer before the timer ran out
            SetButtonsState(false);
        }

    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswers(i);
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
        // Debug.Log("progressBar.value: " + progressBar.value);
        // Debug.Log("progressBar.maxValue: " + progressBar.maxValue);
        // Debug.Log("questions.Count: " + questions.Count);
        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
        if (questions.Count > 0)
        {
            SetButtonsState(true); // Enable buttons for the next question
            SetDefaultButtonColor(); // Set default color for buttons
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }

    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion); // Remove the question from the list to avoid repeating questions
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true; // Set hasAnsweredEarly to true when an answer is selected
        DisplayAnswer(index);
        SetButtonsState(false); // Disable buttons after an answer is selected
        timer.CancelTimer(); // Reset the timer
    }

    private void DisplayAnswer(int index)
    {
        Debug.Log("Progress bar value changed");
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "回答正确!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.color = correctAnswerColor;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswers(currentQuestion.GetCorrectAnswerIndex());
            questionText.text = "回答错误，正确答案是:\n" + correctAnswer;
            Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.color = correctAnswerColor;
        }
        scoreText.text = "得分: " + scoreKeeper.CalculateScore() + "%";
    }
}
