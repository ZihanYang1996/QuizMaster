using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    [SerializeField] Button replayButton; // We need to add a reference to the replay button in the Unity Editor

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        replayButton.onClick.AddListener(OnReplayLevel); // Instead of using the Unity Editor to add the listener, we can add it here
        // replayButton.onClick.AddListener(() => OnReplayLevelWithIndex(0)); // Using a lambda expression to call the OnReplayLevelWithIndex method with the index 0
        // replayButton.onClick.AddListener(delegate { OnReplayLevelWithIndex(0); }); // Using a delegate to call the OnReplayLevelWithIndex method with the index 0
        
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFInalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnReplayLevelWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
