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
    public float initial_time;
    public float final_time;
    public float current_time;
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
        QUIZ_MAX_TIME = 30.0f;
        timeQuiz = QUIZ_MAX_TIME;
    }


    void Start(){
        initial_time = GAME_MAX_TIME;
        final_time = initial_time - QUIZ_TIME;
    }
    // t_inicial = 120
    // t_final = t_inicial - QUIZ_TIME
    // if (t_atual <= t_final)

    void Update() {
        amountOfQuestions = QuizManager.GetInstance().QuestionAndAnswersList.Count;
        current_time = GameDetails.GetInstance().RemainingTimeFloat;
        if (current_time < final_time && amountOfQuestions > 0) {
            final_time = final_time - QUIZ_TIME;
            startCountdown = true;
            quizPause = true;
            QuizWindowGame.GetInstance().Show();
            
            if(answered) {
                QuizManager.GetInstance().generateQuestion();
                answered = false;
            }
        } else if (quizPause == false) {
            QuizManager.GetInstance().timeQuiz = QUIZ_MAX_TIME;
            startCountdown = false;
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