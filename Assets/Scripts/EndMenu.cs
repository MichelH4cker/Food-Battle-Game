using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour {
    
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
