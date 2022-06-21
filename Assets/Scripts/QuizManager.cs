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

    public float QUIZ_TIME;
    public float timeQuiz;
    public int currentQuestion;

    public Text QuestionText;
    public Text CorrectFeedbackText;
    public Text WrongFeedbackText;
    public Text TimeIsOverFeedbackText;
    public Text QuizTimeText;
    public Text TimeIsOverText;

    public enum QuizFeedback {
        Correct,
        Wrong,
        TimeIsOverWithAlly,
        TimeIsOverWithoutAlly
    }

    void Awake() {
        instance = this;
        CorrectFeedbackText.gameObject.SetActive(false);
        WrongFeedbackText.gameObject.SetActive(false);
        TimeIsOverFeedbackText.gameObject.SetActive(false);
        TimeIsOverText.gameObject.SetActive(false);
    }

    void Start() {
        QUIZ_TIME = GameManager.GetInstance().QUIZ_TIME;
        generateQuestion();
        timeQuiz = GameManager.GetInstance().QUIZ_MAX_TIME;
        QuizTimeText.text = Mathf.Round(timeQuiz) + "s";    
    }

    void Update(){
        if(GameManager.GetInstance().startCountdown){
            timeQuiz -= Time.deltaTime;
        }

        QuizTimeText.text = Mathf.Round(timeQuiz) + "s";    

        if(timeQuiz < 0){
            timeQuiz = GameManager.GetInstance().QUIZ_MAX_TIME;
            GameManager.GetInstance().startCountdown = false;
            GameManager.GetInstance().QUIZ_TIME += QUIZ_TIME;
            GameManager.GetInstance().quizPause = false;
            GameManager.GetInstance().answered = true;
            SoundManager.PlaySound(SoundManager.Sound.WrongAnswer);
            if(GameManager.GetInstance().livingAllies > 0) {
                FriendController.GetInstance().DestroyAlly();
                StartCoroutine(AnswerFeedback(QuizFeedback.TimeIsOverWithAlly));
            } else {
                StartCoroutine(AnswerFeedback(QuizFeedback.TimeIsOverWithoutAlly));
            }
        }
    }

    public void Correct() {
        QuestionAndAnswersList.RemoveAt(currentQuestion);
        GameManager.GetInstance().QUIZ_TIME += QUIZ_TIME;
        GameManager.GetInstance().quizPause = false;
        GameManager.GetInstance().answered = true;
        StartCoroutine(AnswerFeedback(QuizFeedback.Correct));
        EnemyController.GetInstance().DestroyEnemy();
    }

    public void Wrong() {
        GameManager.GetInstance().QUIZ_TIME += QUIZ_TIME;
        GameManager.GetInstance().quizPause = false;
        GameManager.GetInstance().answered = true;
        if(GameManager.GetInstance().livingAllies > 0) {
            StartCoroutine(AnswerFeedback(QuizFeedback.Wrong));
            FriendController.GetInstance().DestroyAlly();
        }        
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

    public void generateQuestion() {
        SoundManager.PlaySound(SoundManager.Sound.Pop);

        currentQuestion = Random.Range(0, QuestionAndAnswersList.Count);

        QuestionText.text = QuestionAndAnswersList[currentQuestion].Question;

        SetAnswers();
    }

    public IEnumerator AnswerFeedback(QuizFeedback result) {
        if(result == QuizFeedback.Correct) {
            CorrectFeedbackText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            CorrectFeedbackText.gameObject.SetActive(false);
        } else if (result == QuizFeedback.Wrong){
            WrongFeedbackText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            WrongFeedbackText.gameObject.SetActive(false);
        } else if (result == QuizFeedback.TimeIsOverWithoutAlly){
            TimeIsOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            TimeIsOverText.gameObject.SetActive(false);
        } else {
            TimeIsOverText.gameObject.SetActive(true);
            TimeIsOverFeedbackText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            TimeIsOverText.gameObject.SetActive(false);
            TimeIsOverFeedbackText.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(3f);
    }

}
