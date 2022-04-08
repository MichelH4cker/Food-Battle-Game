using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {
    
    public Text FeedbackGameText;

    void Start() {
        if (GameDetails.GetInstance().PlayerWon){
            FeedbackGameText.text = "VOCÊ GANHOU!";
        } else {
            FeedbackGameText.text = "VOCÊ PERDEU!";
        }
    }

    public void PlayAgain() {
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    public void BackMenu() {
        SceneLoader.Load(SceneLoader.Scene.MenuScene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
