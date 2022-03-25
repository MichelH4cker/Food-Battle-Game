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
    private float quizTime = 3.0f;
    public bool quizPause;


    void Awake() {
        instance = this;
        quizPause = false;
    }

    void Update() {
        if (Time.time > quizTime && QuizManager.GetInstance().QuestionAndAnswersList.Count < 0) {
            quizTime += 15.0f;
            quizPause = true;
            QuizWindowGame.GetInstance().Show();
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