using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour  {
    public List<GameObject> spawnPointList;
    public List<GameObject> enemiesPrefabs; 

    int spawnPointIndex;
    int enemyIndex;
    float spawnTime = 0;
    float randomTime;

    void Awake() {
        randomTime = 0;
    }

    private void Update(){        
        spawnTime += Time.deltaTime;
        
        if (spawnTime > randomTime) {
            spawnTime = 0;
            randomTime = Random.Range(6,10);

            spawnPointIndex = Random.Range(0,5);
            enemyIndex = Random.Range(0,3);

            Instantiate(enemiesPrefabs[enemyIndex], spawnPointList[spawnPointIndex].transform);
        } 
    }
}
