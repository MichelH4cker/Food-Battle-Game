using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame() {
        SceneLoader.Load(SceneLoader.Scene.GameScene);
    }

    public void CreditosGame(){
        SceneLoader.Load(SceneLoader.Scene.CreditosScene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
