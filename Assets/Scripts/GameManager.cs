using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    public static GameManager GetInstance() {
        return instance;
    }

    public SoundAudioClip[] soundAudioClipArray;

    [Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public List<GameObject> allies;

    public GameObject draggingObject;
    public GameObject currentContainer;

    public float GAME_MAX_TIME;
    public float QUIZ_MAX_TIME;
    public float QUIZ_TIME;
    public float timeQuiz;
    public int livingAllies = 0;
    public bool quizPause;
    public bool startCountdown;
    public bool answered;
    public bool positioned;

    int amountOfQuestions;

    void Awake() {
        instance = this;
        amountOfQuestions = 0;
        answered = true;
        positioned = false;
        quizPause = false;
        startCountdown = false;
        QUIZ_TIME = 15.0f;
        GAME_MAX_TIME = 120.0f;
        QUIZ_MAX_TIME = 5.0f;
        timeQuiz = QUIZ_MAX_TIME;
    }

    void Update() {
        amountOfQuestions = QuizManager.GetInstance().QuestionAndAnswersList.Count;
        if (Time.timeSinceLevelLoad > QUIZ_TIME && amountOfQuestions > 0) {
            startCountdown = true;
            quizPause = true;
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

            allies.Add(objectGame);

            objectGame.GetComponent<FriendController>().enemies = currentContainer.GetComponent<ObjectContainer>().spawnPoint.enemies;
            
            currentContainer.GetComponent<ObjectContainer>().isFull = true;

            positioned = true;
            livingAllies++;
        } else {
            positioned = false;
        }
    }
}