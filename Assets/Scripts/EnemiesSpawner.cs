using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour  {
    private static EnemiesSpawner instance;

    public static EnemiesSpawner GetInstance() {
        return instance;
    }

    public List<SpawnPoint> spawnPointList;
    public List<GameObject> enemiesPrefabs; 

    public GameObject enemyInstance;
    
    private bool quizPause;
 
    int spawnPointIndex;
    int enemyIndex;
    float spawnTime = 0;
    float randomTime;

    void Awake() {
        randomTime = 0;
        instance = this;
    }

    void Start() {
        quizPause = GameManager.GetInstance().quizPause;
    }

    private void Update(){      
        quizPause = GameManager.GetInstance().quizPause;
        
        if(quizPause == false){
            SpawnEnemies();
        }  
    }

    public void SpawnEnemies() {
        spawnTime += Time.deltaTime;
            
        if (spawnTime > randomTime) {
            spawnTime = 0;
            randomTime = Random.Range(8,13);

            spawnPointIndex = Random.Range(0,5);
            enemyIndex = Random.Range(0,3);

            enemyInstance = Instantiate(enemiesPrefabs[enemyIndex], spawnPointList[spawnPointIndex].transform);

            spawnPointList[spawnPointIndex].enemies.Add(enemyInstance);

        } 
    }

}
