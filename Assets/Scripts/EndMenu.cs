using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {
    
    public Text FeedbackGameText;

    void Start() {
        if (GameDetails.GetInstance().PlayerWon){
            FeedbackGameText.text = "VOCÊ CONSEGUIU APRENDER TUDO SOBRE NUTRIÇÃO PERFEITAMENTE E INGERIU SOMENTE ALIMENTOS ORGÂNICOS E NÃO INDUSTRIAIS. VOCÊ GANHOU!";
        } else {
            FeedbackGameText.text = "VOCÊ INGERIU ALIMENTOS PROCESSADOS E NÃO SAUDÁVEIS, PREJUDICANDO SUA SAÚDE. VOCÊ PERDEU!";
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
