using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDetails : MonoBehaviour {
    
    public Text RemainingTimeText;
    public Text RemainingAllyText;
    private float RemainingTimeFloat;
 
    void Start() {
        RemainingTimeFloat = GameManager.GetInstance().GAME_MAX_TIME;
        RemainingTimeText.text = "TEMPO RESTANTE: " + RemainingTimeFloat + "s";        
        InvokeRepeating("DecreaseTimeRemaining", 1.0f, 1.0f);
    }

    void Update() {
        RemainingTimeText.text = "TEMPO RESTANTE: " + RemainingTimeFloat + "s";        
    }

    void DecreaseTimeRemaining(){
        if(RemainingTimeFloat > 0) {
            RemainingTimeFloat -= 1;
        }
    }

}