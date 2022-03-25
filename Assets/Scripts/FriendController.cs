using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController : MonoBehaviour {
    public GameObject bullet;
    public GameObject toAttack;

    public GameManager gameManager;

    public List<GameObject> enemies;

    public bool isAttacking;
    public int Health;
    public int DamageValue;
    public float attackCooldown;

    private float attackTime;
    private bool quizPause;
    
    private void Update() {
        quizPause = GameManager.GetInstance().quizPause;

        if (!quizPause) { // executa somente se o jogo não estiver pausado pelo quiz
            enemies.Add(EnemiesSpawner.GetInstance().enemyInstance);
            if (enemies.Count > 0){  
                float distance = 1000;
                foreach (GameObject enemy in enemies) {
                    float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (enemyDistance < distance) {
                        toAttack = enemy;
                        distance = enemyDistance;
                    }
                }
            } else {
                toAttack = null;
            }

            if (toAttack != null) {
                if (attackTime <= Time.time) {
                    GameObject bulletInstance = Instantiate(bullet, transform);
                    bulletInstance.GetComponent<Bullet>().DamageValue = DamageValue;
                    attackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    void Start() {
        gameManager = GameManager.instance;
    }

    public void ReceiveDamage(int Damage) { // friend receive damage
        if(Health - Damage <= 0) {
            Destroy(this.gameObject);
        } else {
            Health = Health - Damage;
        }
    }
}