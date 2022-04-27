using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {
    
    public enum Scene {
        MenuScene,
        LoadingScene,
        GameScene,
        EndScene,
        CreditosScene,
    }

    private static Scene targetScene;

    public static void Load(Scene scene) {
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
       
        targetScene = scene;
    }

    public static void LoadTargetScene() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
