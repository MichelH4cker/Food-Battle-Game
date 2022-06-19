using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Enemy {
    public EnemyType enemyType;
    public int spawnTime;
    public int spawner;
    public bool randomSpawn; 
    public bool isSpawned;
}

public enum EnemyType {
    EnemyFrenchFries,
    EnemySoda
}