using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] option;
    public int currentQuestion;
    public TextMeshProUGUI QuestionTxT;
    public TextMeshProUGUI ScoreText;
    public int score = 0;
    public int numCorrect = 0;
    public int numWrong = 0;
    public int numQuestions = 0;

    private void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        // Update the score and number of correct answers
        score += 10;
        numCorrect++;

        // Remove the current question from the list of questions
        QnA.RemoveAt(currentQuestion);

        // Check if the score is greater than or equal to 40
        if (score >= 40)
        {
            SceneManager.LoadScene("Scene1");
            return;
        }

        // Generate the next question
        generateQuestion();
    }

    public void incorrect()
    {
        // Update the score and number of wrong answers
        score -= 5;
        numWrong++;

        // Check if the score is greater than or equal to 40
        if (score < 40)
        {
            SceneManager.LoadScene("Scene2");
            return;
        }

        // Generate the next question
        generateQuestion();
    } 

    void SetAnswers()
    {
        for (int i = 0; i < option.Length; i++)
        {
            option[i].GetComponent<AnswerScript>().isCorrect = false;
            option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                option[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count == 0)
        {
            endGame();
            return;
        }

        // Get the next question and display it
        currentQuestion = 0;
        QuestionTxT.text = QnA[currentQuestion].Question;
        SetAnswers();

        // Update the number of questions
        numQuestions++;
    }

    void endGame()
    {
        // Check if the score is greater than or equal to 40
        if (score < 40)
        {
            SceneManager.LoadScene("Scene2");
        }
        else
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}