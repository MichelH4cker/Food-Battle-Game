using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

    public static QuizManager instance;

    public static QuizManager GetInstance() {
        return instance;
    }

    public List<QuestionAndAnswers> QuestionAndAnswersList;
    public GameObject[] answerOptions;
    public int currentQuestion;

    public Text QuestionText;

    void Awake() {
        instance = this;
    }

    void Start() {
        generateQuestion();
    }

    public void correct() {
        QuestionAndAnswersList.RemoveAt(currentQuestion);
        GameManager.GetInstance().quizPause = false;
    }

    void SetAnswers() {
        for (int i = 0; i < answerOptions.Length; i++) {
            answerOptions[i].GetComponent<AnswerScript>().isCorrect = false;
            answerOptions[i].transform.GetChild(0).GetComponent<Text>().text = QuestionAndAnswersList[currentQuestion].Answers[i]; 

            if(QuestionAndAnswersList[currentQuestion].CorrectAnswer == i + 1) {
                answerOptions[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion() {
        currentQuestion = Random.Range(0, QuestionAndAnswersList.Count);

        QuestionText.text = QuestionAndAnswersList[currentQuestion].Question;

        SetAnswers();
    }

}
