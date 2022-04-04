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

    void Start() {
        gameManager = GameManager.instance;
        quizPause = GameManager.GetInstance().quizPause;
    }

    private void Update() {
        if (!quizPause) {
            if (enemies.Count > 0){  
                float distance = 1300;
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

    public void ReceiveDamage(int Damage) { // friend receive damage
        if(Health - Damage <= 0) {
            Destroy(this.gameObject);
        } else {
            Health = Health - Damage;
        }
    }
}