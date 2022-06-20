using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour {

    public bool isCorrect;
    public QuizManager quizManager;

    public void Answer() {
        if(isCorrect) {
            Debug.Log("Correct Answer!");
            SoundManager.PlaySound(SoundManager.Sound.CorrectAnswer);
            quizManager.Correct();
        } else {
            Debug.Log("Wrong Answer!");
            SoundManager.PlaySound(SoundManager.Sound.WrongAnswer);
            quizManager.Wrong();
        }
    }
}
