using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    public static GameManager GetInstance() {
        return instance;
    }

    public GameObject draggingObject;
    public GameObject currentContainer;

    public float GAME_MAX_TIME = 120.0f;
    public float QUIZ_TIME;
    public bool quizPause;
    public bool answered;
    public bool positioned;

    int amountOfQuestions;

    void Awake() {
        instance = this;
        amountOfQuestions = 0;
        answered = true;
        positioned = false;
        quizPause = false;
        QUIZ_TIME = 15.0f;
    }

    void Update() {
        amountOfQuestions = QuizManager.GetInstance().QuestionAndAnswersList.Count;

        if (Time.timeSinceLevelLoad > QUIZ_TIME && amountOfQuestions > 0) {
            quizPause = true;
            QuizWindowGame.GetInstance().Show();
            if(answered) {
                Time.timeScale = 0;
                QuizManager.GetInstance().generateQuestion();
                answered = false;
            }
        } else if (quizPause == false) {
            QuizWindowGame.GetInstance().Hide();
        }
    }

    public void PlaceObject() {
        if (draggingObject != null && currentContainer != null) {
            GameObject objectGame = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.objectGame, currentContainer.transform);

            objectGame.GetComponent<FriendController>().enemies = currentContainer.GetComponent<ObjectContainer>().spawnPoint.enemies;
            
            currentContainer.GetComponent<ObjectContainer>().isFull = true;

            positioned = true;
        } else {
            positioned = false;
        }
    }
}