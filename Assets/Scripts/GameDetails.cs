using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDetails : MonoBehaviour {
    
    public static GameDetails instance;

    public static GameDetails GetInstance() {
        return instance;
    }

    public Text RemainingTimeText;
    public Text ErrorMessageText;

    public bool PlayerWon;

    private float RemainingTimeFloat;
    private float ErrorMessageDelay;
    private float counterTime;
 
    void Awake() {
        ErrorMessageDelay = 5.0f;
        instance = this;
    }

    void Start() {
        HideErrorMessage();
        RemainingTimeFloat = GameManager.GetInstance().GAME_MAX_TIME;
        RemainingTimeText.text = "TEMPO RESTANTE: " + RemainingTimeFloat + "s";        
        InvokeRepeating("DecreaseTimeRemaining", 1.0f, 1.0f);
    }

    void Update() {
        if(RemainingTimeFloat <= 0) {
            SoundManager.PlaySound(SoundManager.Sound.GameWon);
            PlayerWon = true;
            SceneLoader.Load(SceneLoader.Scene.EndScene);
        }
        RemainingTimeText.text = "TEMPO RESTANTE: " + RemainingTimeFloat + "s";     
        counterTime += Time.deltaTime;
        if (counterTime > ErrorMessageDelay) {
            counterTime = 0;
            HideErrorMessage();
        }   
    }

    void DecreaseTimeRemaining(){
        if(RemainingTimeFloat > 0) {
            RemainingTimeFloat -= 1;
        }
    }

    public void ShowErrorMessage() {
        ErrorMessageText.gameObject.SetActive(true);
    }

    public void HideErrorMessage() {
        ErrorMessageText.gameObject.SetActive(false);
    }

}