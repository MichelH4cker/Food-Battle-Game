using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public Transform pauseMenu;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenu.gameObject.activeSelf){
                pauseMenu.gameObject.SetActive(false);
                GameManager.GetInstance().quizPause = false;
            } else {
                pauseMenu.gameObject.SetActive(true);
                GameManager.GetInstance().quizPause = true;
            }
        }
    }

    public void ResumeGame(){
        pauseMenu.gameObject.SetActive(false);
        GameManager.GetInstance().quizPause = false;
    }

    public void Menu(){
        SceneLoader.Load(SceneLoader.Scene.MenuScene);
    }
}
