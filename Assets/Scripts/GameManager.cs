using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject draggingObject;
    public GameObject currentContainer;

    public int currentAllies = 0;
    private float quizzTime = 10.0f;
    public bool quizPause;

    public static GameManager GetInstance() {
        return instance;
    }

    void Awake() {
        instance = this;
        quizPause = false;
    }

    void Update() {
        if (Time.time > quizzTime) {
            quizPause = true;
            QuizWindowGame.GetInstance().Show();
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