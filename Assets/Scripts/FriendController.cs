using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour {
    public GameObject bullet;
    public List<GameObject> enemies;
    public GameObject toAttack;
    public bool isAttacking;
    public int DamageValue;
    public float attackCooldown;
    private float attackTime;

    private void Update() {
        if (enemies.Count > 0){  
            float distance = 999;
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
