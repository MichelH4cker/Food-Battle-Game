using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizWindowGame : MonoBehaviour {
    private static QuizWindowGame instance;
    
    public static QuizWindowGame GetInstance() {
        return instance;
    }

    void Awake() {
        instance = this;
    }

    void Start() {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
