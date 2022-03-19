using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour  {
    public List<GameObject> enemiesPrefabs; 
    public List<Enemy> enemies;

    private void Update(){
        foreach(Enemy enemy in enemies) {
            if(enemy.isSpawned == false && enemy.spawnTime <= Time.time) {
                if(enemy.randomSpawn) {
                    enemy.spawner = Random.Range(0, transform.childCount);
                }

                GameObject enemyInstance = Instantiate(enemiesPrefabs[(int)enemy.enemyType], transform.GetChild(enemy.spawner).transform);

                transform.GetChild(enemy.spawner).GetComponent<SpawnPoint>().enemies.Add(enemyInstance);

                enemy.isSpawned = true;
            }
        }
    }
}
