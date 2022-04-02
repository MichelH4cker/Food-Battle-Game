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

    public int currentAllies = 0;
    public float QUIZ_TIME;
    public bool quizPause;
    public bool answered;

    void Awake() {
        instance = this;
        answered = true;
        quizPause = false;
        QUIZ_TIME = 15.0f;
    }

    void Update() {
        if (Time.time > QUIZ_TIME && QuizManager.GetInstance().QuestionAndAnswersList.Count > 0) {
            quizPause = true;
            Time.timeScale = 0;
            QuizWindowGame.GetInstance().Show();
            if(answered) {
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

            currentAllies++;
        }
    }
}